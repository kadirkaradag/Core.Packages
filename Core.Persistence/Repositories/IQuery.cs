namespace Core.Persistence.Repositories;

public interface IQuery<T> // raporlama işleri vs için
{
    IQueryable<T> Query();
}
