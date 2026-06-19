namespace Service.Application.TeaMatchService.Dto
{
    public record MatchingTeaDto
    {
        public Guid TeaId {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Image {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public decimal MatchPercent {get; set;}
    }
}