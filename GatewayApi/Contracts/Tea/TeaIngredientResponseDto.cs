namespace GateWayApi.Contracts.Tea
{
    public class TeaIngredientResponseDto
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}