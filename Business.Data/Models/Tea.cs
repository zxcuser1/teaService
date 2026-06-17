using Business.Data.BaseEntities;

namespace Business.Data.Models
{
    public class Tea : BaseEntity
    {
        public string Name {get; set;} = string.Empty;
        public string Image {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
    }
}