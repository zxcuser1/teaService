using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Business.Data.Models;
using Microsoft.IdentityModel.Tokens;
using Service.Application.Interfaces;

namespace Service.Application.TokenService
{
    public class TokenHelper
    {
        private readonly JwtParams _accessTokenParams;
        
        public TokenHelper(JwtParams accessTokenParams)
        {
            _accessTokenParams = accessTokenParams;
        }
        public string GenerateAccessToken(User user)
        {
            var now = DateTime.UtcNow;
            var creds = new SigningCredentials(_accessTokenParams.KEY, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
              new(ClaimTypes.Role, user.RoleId.ToString()) ,
              new(ClaimTypes.NameIdentifier, user.Guid.ToString()),
            };

            var accessToken = new JwtSecurityToken(
                issuer: _accessTokenParams.ISSUER,
                audience: _accessTokenParams.AUDIENCE,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(_accessTokenParams.LIFETIME)),
                signingCredentials: creds
            );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
            return encodedToken;
        }

        public static string CreateSHA512(string input)
        {
            return Convert.ToHexString(SHA512.HashData(Encoding.ASCII.GetBytes(input)));
        }
    }
}