using Business.Data.BaseEntities;
using Business.Data.Enums;

namespace Business.Data.Models
{
    public class Tea : BaseEntity
    {
        public string Name {get; set;} = string.Empty;
        public string ImageUrl {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public bool IsDeleted {get; set;} = false;
        public ModerationStatus ModerationStatus {get;set;} = ModerationStatus.Pending;
        public List<TeaIngredient> TeaIngredients {get; set;} = [];
    }
}