namespace GateWayApi.Contracts.Tea
{
    public class TeaDetailsResponseDto
    {
        public Guid TeaId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public List<TeaIngredientResponseDto> Ingredients { get; set; } = [];
    }
}