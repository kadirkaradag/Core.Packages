namespace Core.Persistence.Dynamic;

public class Filter  // mesela vites e göre filtreleme
{
    public string Field { get; set; }  // vites alanı
    public string? Value { get; set; }  // vites alanı değeri
    public string Operator { get; set; }  // büyüktür küçüktür eşittir vs.
    public string? Logic { get; set; } // yani şu şartı ve şu şartı sağlayanları getir gibi AND OR logicleri
    public IEnumerable<Filter>? Filters { get; set; } // bir filtreye baska filtreler de uyguyalabiliriz

    public Filter()
    {
        Field = string.Empty;
        Operator = string.Empty;
    }

    public Filter(string field, string @operator)  // operator kelimesi c# da kullanılan bi keyword o yüzden başına @ koyarak bizim değişkenimiz oldugunu belirtiyoruz.
    {
        Field = field;
        Operator = @operator;
    }
}
