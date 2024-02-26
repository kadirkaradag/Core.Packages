using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Core.Application.Pipelines.Caching;

public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly CacheSettings _cacheSettings;
    private readonly IDistributedCache _cache; // distributedCache yapısı: farklı cache ortamlarını, inMemory , redis gibi istedigimiz zaman istedigmize geçebiliyoruz.
    private readonly ILogger<CachingBehaviour<TRequest, TResponse>> _logger;

    public CachingBehaviour(IDistributedCache cache, ILogger<CachingBehaviour<TRequest, TResponse>> logger, IConfiguration configuration)
    {
        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>() ?? throw new InvalidOperationException();       
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache) // bypass işaretliyse hiç cache işine girme kodu calıstır.
        {
            return await next();
        }

        TResponse response;

        //cacheler byte array şeklinde tutulur*
        byte[]? cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
        if (cachedResponse != null)
        {
            response = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse)); //byte array olarak alınan cache i deserialize et
            _logger.LogInformation($"Cached from Cache -> {request.CacheKey}");
        }
        else
        {
            response = await GetResponseAndAddToCache(request, next, cancellationToken);  // cache yoksa git verili al cache e ekle
        }

        return response;
    }

    private async Task<TResponse?> GetResponseAndAddToCache(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next(); // methodu calıstır
        
        TimeSpan? slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration); //appsettings den okuyacağız.

        DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };

        byte[] serializedData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));

        await _cache.SetAsync(request.CacheKey, serializedData, cacheOptions, cancellationToken);

        _logger.LogInformation($"Added to Cache -> {request.CacheKey}");


        return response;
    }
}
