using System.ComponentModel.DataAnnotations.Schema;
using Business.Data.BaseEntities;

namespace Business.Data.Models
{
    public class TeaIngredient : BaseEntity
    {
        public decimal Amount {get; set;}
        
        [ForeignKey(nameof(IngredientId))]
        public Ingredient Ingredient {get; set;}
        public Guid IngredientId {get; set;}

        [ForeignKey(nameof(TeaId))]
        public Tea Tea {get; set;}
        public Guid TeaId {get; set;}
    }
}
