using System.ComponentModel.DataAnnotations;
using Businnes.Data.Iterfaces;

namespace Businnes.Data.BaseEntities
{
    public class BaseEntity : Entity, IBaseEntity
    {
        [Key]
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}