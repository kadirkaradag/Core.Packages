namespace Core.Persistence.Paging;

public abstract class BasePageableModel  // ihtiyaca göre ezilebilmesi için abstract yapıyoruz
{
    public int Size { get; set; }
    public int Index { get; set; }
    public int Count { get; set; }
    public int Pages { get; set; }
    public bool HasPrevious => Index > 0;
    public bool HasNext => Index + 1 < Pages;
}
