using System.ComponentModel.DataAnnotations;

namespace Business.Data.Interfaces
{
    public interface IBaseEntity
    {
        [Key]
        Guid Guid { get; set; }
    }
}
