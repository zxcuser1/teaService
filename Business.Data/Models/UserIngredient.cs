using System.ComponentModel.DataAnnotations.Schema;
using Business.Data.BaseEntities;

namespace Business.Data.Models
{
    public class UserIngredient : BaseEntity
    {
        public decimal Amount {get; set;}
        public Guid UserId {get; set;}

        [ForeignKey(nameof(UserId))]
        public User User {get; set;}

        [ForeignKey(nameof(IngredientId))]
        public Ingredient Ingredient {get; set;}
        public Guid IngredientId {get; set;}
    }
}
