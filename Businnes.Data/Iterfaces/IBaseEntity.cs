using System.ComponentModel.DataAnnotations;

namespace Businnes.Data.Iterfaces
{
    public interface IBaseEntity
    {
        [Key]
        Guid Guid { get; set; }
    }
}