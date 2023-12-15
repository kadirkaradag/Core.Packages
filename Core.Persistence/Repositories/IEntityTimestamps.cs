namespace Core.Persistence.Repositories;

public interface IEntityTimestamps //DeletedDate gibi proplara zaman damgası verebilmek için, imzalama tekniği yapacağız.
{
    DateTime CreatedDate { get; set; }
    DateTime? UpdatedDate { get; set; }
    DateTime? DeletedDate { get; set; }
}
