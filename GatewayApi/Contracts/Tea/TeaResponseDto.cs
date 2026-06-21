namespace GateWayApi.Contracts.Tea
{
    public class TeaResponseDto
    {
        public Guid TeaId {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public string Image {get; set;} = string.Empty;
    }
}