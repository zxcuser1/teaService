namespace GateWayApi.Contracts.Tea
{
    public class TeaRequestDto
    {
        public Guid TeaId {get; set;}
        public string? Name {get; set;}
        public string? Description {get; set;}
        public string? Image {get; set;}
    }
}