using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Service.Application.TokenService
{
    public class JwtParams(string issuer, string audience, string key)
    {
        public string ISSUER = issuer;
        public string AUDIENCE = audience;
        public SymmetricSecurityKey KEY = new(Encoding.ASCII.GetBytes(key));
        public int LIFETIME = 5;
    }
}