using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Dynamic;

public class Sort // A'dan Z'ye , Z'den A'ya, 0 dan 9 a 9 dan 0 a sırala gibi
{
    public string Field { get; set; }
    public string Dir { get; set; }
    public Sort()
    {
        Field = string.Empty;
        Dir = string.Empty;
    }

    public Sort(string field, string dir)  
    {
        Field = field;
        Dir = dir;
    }
}
