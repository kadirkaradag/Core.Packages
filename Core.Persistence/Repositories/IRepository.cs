using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public interface IRepository<TEntity, TEntityId> : IQueryable<TEntity> where TEntity : Entity<TEntityId>
{
    TEntity? Get(
        Expression<Func<TEntity, bool>> predicate, // lambda ile where kosulu gecicez, deletagion alacak, TEntity dönecek ve boolean bir predicate olacak.
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, //IIncludableQueryable ile join e göre data getirmek isteyebilirim, IIncludableQueryable ile verileri query edebilecek ortamı da sağlıyoruz. include null verildi illa join atmak zorunda değiliz.
        bool withDeleted = false, // softdelete ile calıstıgımız icin db de silinenleri sorgularda getireyim mi getirmeyeyim mi diye soruyor. default u false.
        bool enableTracking = true, //
        CancellationToken cancellationToken = default // asenkron operasyonlarda iptal etme işlemi için gereken değer.
       );

    Paginate<TEntity> GetList( //sayfa olarak getiriyoruz IPaginate ile
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, // tüm sayfayı çekmek yerine sayfalı calısacagız
        int size = 10, // her sayfada kaç veri olacak
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Paginate<TEntity> GetListByDynamic( // çok önemli , dinamik sorgulama. yani mesela arabanın vitesi, modeli , km si , fiyatı tek tek hepsini iceren select yerine dynamic sorgu yapıyoruz
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0, // tüm sayfayı çekmek yerine sayfalı calısacagız
        int size = 10, // her sayfada kaç veri olacak
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    bool Any( // böyle bir data var mı yok mu, atıyorum tckn var mı yok mu 
        Expression<Func<TEntity, bool>>? predicate = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    TEntity Add(TEntity entity);

    ICollection<TEntity> AddRange(ICollection<TEntity> entity);

    TEntity Update(TEntity entity);

    ICollection<TEntity> UpdateRangec(ICollection<TEntity> entity);

    TEntity Delete(TEntity entity, bool permanent = false); // permanent kalıcı demek, yani db den sileyim mi yoksa silindi diye işaretleyeyim mi. false diyerek sadece işaretle diyoruz.

    ICollection<TEntity> DeleteRange(ICollection<TEntity> entity, bool permanent = false);
}
