
using Business.Data.BaseEntities;

namespace Business.Data.Models
{
    public class Ingredient : BaseEntity
    {
        public string Name {get;set;} = string.Empty;
        public bool IsDeleted {get;set;} = false;
        public bool IsModerated {get;set;} = false;
        public string ImageUrl {get;set;} = string.Empty;
        public Guid? SuggestedUserId {get;set;}
    }
}
