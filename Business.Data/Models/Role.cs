using Business.Data.BaseEntities;

namespace Business.Data.Models
{
    public class Role : BaseEntity
    {
        public string Name {get; set;} = string.Empty;
    }
}