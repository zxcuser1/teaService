namespace Service.Application.Helpers
{
    public class PasswordDto
    {
        public string PasswordHash {get; set;} = string.Empty;
        public byte[] Salt {get; set;} = [];
    }
}