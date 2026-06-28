namespace GateWayApi.Contracts.Ingredient
{
    public class IngredientResponseDto
    {
        public Guid IngredientId {get; set;}
        public string Name {get; set;} = string.Empty;
        public string ImageUrl {get; set;} = string.Empty;
        public Guid? SuggestedUserId {get; set;}
    }
}
