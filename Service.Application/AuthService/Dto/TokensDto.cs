namespace Service.Application.AuthService.Dto
{
    public class TokenDto
    {
        public string AccessToken {get; set;}
        public string RefreshTokenHash {get; set;}
    }
}