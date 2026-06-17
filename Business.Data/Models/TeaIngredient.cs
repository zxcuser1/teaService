using System.ComponentModel.DataAnnotations.Schema;
using Business.Data.BaseEntities;

namespace Business.Data.Models
{
    public class TeaIngredient : BaseEntity
    {
        public decimal Amount {get; set;}
        
        [ForeignKey("Ingredient")]
        public Ingredient Ingredient {get; set;}
        public Guid IngredientId {get; set;}

        [ForeignKey("Tea")]
        public Tea Tea {get; set;}
        public Guid TeaId {get; set;}
    }
}
