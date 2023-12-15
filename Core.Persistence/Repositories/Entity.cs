using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories;

public class Entity<TId> : IEntityTimestamps
{
    public TId Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }

    public Entity() // hiç bir şey verilmezse, o id nin int se mesela default u 0 ise onu versin
    {
        Id = default;
    }

    public Entity(TId id)
    {
        Id= id;
    }


}
