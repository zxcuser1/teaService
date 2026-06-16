using System.ComponentModel.DataAnnotations;
using Business.Data.Interfaces;

namespace Business.Data.BaseEntities
{
    public class BaseEntity : Entity, IBaseEntity
    {
        [Key]
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
