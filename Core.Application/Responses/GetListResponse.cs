using Core.Persistence.Paging;

namespace Core.Application.Responses;

public class GetListResponse<T> :BasePageableModel
{
    private IList<T> _items;

    public IList<T> Items 
    {
        get => _items?? new List<T>(); //item yoksa yeni boş bir liste döndür.
        set => _items = value;
    }
}
