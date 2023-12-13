namespace Core.Persistence.Dynamic;

public class DynamicQuery
{
    public IEnumerable<Sort>? Sort { get; set; }
    public Filter? Filter { get; set; }

    public DynamicQuery()
    {
        
    }

    public DynamicQuery(IEnumerable<Sort>? sort, Filter? filter)
    {
        Filter = filter;
        Sort = sort;
    }
}


// örneğin ado.net de
// select * from arabalar where price <100 and (transmission = 'automatic' or x) gibi arama alanlarında ne doldurulmussa böyle sorgu yapılır
//entity frameworkde
//p=> p.price<100 && () seklinde yapılıyor

//Linq.DynamicCore kütüphanesi diyor ki bana string olarak yani adonet olarak ver ben sana linq döndüreyim diyor.