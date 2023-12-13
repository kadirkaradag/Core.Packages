using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Paging;
//extension oldugu icin static
public static class IQueryablePaginateExtensions // IQueryable da tüm datayı cekip sonra filtrelemiyor da filtrelerini belirtiyoruz gidip db den direkt filtreli cekiyor.
{
    public static async Task<Paginate<T>> ToPaginateAsync<T>(
        this IQueryable<T> source,
        int index,
        int size,
        CancellationToken cancellationToken = default
        )
    {
        int count = await source.CountAsync(cancellationToken ).ConfigureAwait( false );
        List<T> items = await source.Skip(index * size).Take(size).ToListAsync().ConfigureAwait( false ); // mesela 5. sayfada isek her sayfada 10 veri varsa onları itemlere koymaya gerek yok 5.10 diyip onları skipliyoruz.

        Paginate<T> list = new()
        {
            Items = items,
            Count = count,
            Index = index,
            Size = size,
            Pages = (int)Math.Ceiling(count / (double)size) // ceiling ile yukarı yuvarlıyoruz
        };

        return list;
    }

    public static Paginate<T> ToPaginate<T>(this IQueryable<T> source,int index,int size)
    {
        int count = source.Count();
        var items =  source.Skip(index * size).Take(size).ToList();

        Paginate<T> list = new()
        {
            Items = items,
            Count = count,
            Index = index,
            Size = size,
            Pages = (int)Math.Ceiling(count / (double)size) 
        };

        return list;
    }
}
