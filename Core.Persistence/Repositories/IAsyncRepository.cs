using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public interface IAsyncRepository<TEntity, TEntityId>: IQuery<TEntity> where TEntity : Entity<TEntityId> //Hangi tip ile çalışacağız ? TEntity tipi ile. Bu crud işlemlerinin çoğu Id ile gerçekleşir onun da veri tipini vermek istiyoruz 
    //TEntity, daha önce oluşturduğumuz Entity<TId> den inherit edilmesini bekliyoruz. Bunu yaparak geliştiricinin kafasına göre bir şey yazmamasını sağlıyoruz sadece Entity<> den inherit edilmiş domain nesnesi istiyoruz.
{
    Task<TEntity?> GetAsync(
         Expression<Func<TEntity, bool>> predicate, // lambda ile where kosulu gecicez, deletagion alacak, TEntity dönecek ve boolean bir predicate olacak.
         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, //IIncludableQueryable ile join e göre data getirmek isteyebilirim, IIncludableQueryable ile verileri query edebilecek ortamı da sağlıyoruz. include null verildi illa join atmak zorunda değiliz.
         bool withDeleted = false, // softdelete ile calıstıgımız icin db de silinenleri sorgularda getireyim mi getirmeyeyim mi diye soruyor. default u false.
         bool enableTracking = true, //
         CancellationToken cancellationToken = default // asenkron operasyonlarda iptal etme işlemi için gereken değer.
        );

    Task<Paginate<TEntity>> GetListAsync( //sayfa olarak getiriyoruz IPaginate ile
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, // tüm sayfayı çekmek yerine sayfalı calısacagız
        int size = 10, // her sayfada kaç veri olacak
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<Paginate<TEntity>> GetListByDynamic( // çok önemli , dinamik sorgulama. yani mesela arabanın vitesi, modeli , km si , fiyatı tek tek hepsini iceren select yerine dynamic sorgu yapıyoruz
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, // tüm sayfayı çekmek yerine sayfalı calısacagız
        int size = 10, // her sayfada kaç veri olacak
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<bool> AnyAsync( // böyle bir data var mı yok mu, atıyorum tckn var mı yok mu 
        Expression<Func<TEntity, bool>>? predicate = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<TEntity> AddAsync(TEntity entity);

    Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);

    Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false); // permanent kalıcı demek, yani db den sileyim mi yoksa silindi diye işaretleyeyim mi. false diyerek sadece işaretle diyoruz.

    Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false);

}
