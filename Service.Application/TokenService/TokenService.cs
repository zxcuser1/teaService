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
        private readonly IUserSessionRepository _sessionRepo;
        private readonly IRepository<RefreshToken> _tokenRepo;
        private readonly IUnitOfWork _unitOfWork;
        public TokenHelper
        (
            JwtParams accessTokenParams,
            IUserSessionRepository sessionRepo,
            IRepository<RefreshToken> tokenRepo,
            IUnitOfWork unitOfWork
        )
        {
            _accessTokenParams = accessTokenParams;
            _sessionRepo = sessionRepo;
            _tokenRepo = tokenRepo;
            _unitOfWork = unitOfWork;
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

        public async Task<string> GenerateRefreshTokenAsync(Guid userId, string deviceId, CancellationToken token)
        {
            try {
                await _unitOfWork.BeginTransactionAsync(token);

                var session = await _sessionRepo.GetUserSessionAsync(userId, deviceId, token);

                if (session == null)
                {
                    session = new UserSession
                    {
                        DeviceId = deviceId,
                        UserId = userId
                    };
                    await _sessionRepo.AddAsync(session);
                    await _unitOfWork.SaveChangesAsync(token);            
                }

                var now = DateTime.UtcNow;

                var rawToken = Convert.ToHexString(
                    RandomNumberGenerator.GetBytes(32));
                
                var tokenHash = CreateSHA512(rawToken);

                var newToken = new RefreshToken
                {
                    TokenHash = tokenHash,
                    ExpiresAt = now.AddDays(30),
                    UserSessionId = session.Guid,
                };

                if (session.CurrentRefreshTokenId != null)
                {
                    var currentToken = await _tokenRepo.GetByIdAsync((Guid)session.CurrentRefreshTokenId, token) 
                        ?? throw new Exception($"Found session {session.Guid} with invalid token id {session.CurrentRefreshTokenId}"); 
                        //TODO make custom exception and specific logic to invalidate session!!

                    currentToken.IsRevoked = true;
                    currentToken.RevokedAt = now;
                    newToken.PreviousTokenId = currentToken.Guid;
                }

                session.CurrentRefreshTokenId = newToken.Guid;

                await _tokenRepo.AddAsync(newToken, token);

                await _unitOfWork.SaveChangesAsync(token);

                await _unitOfWork.CommitAsync(token);
                return rawToken;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync(CancellationToken.None);
                throw;
            }
        }

        public static string CreateSHA512(string input)
        {
            return Convert.ToHexString(SHA512.HashData(Encoding.ASCII.GetBytes(input)));
        }
    }
}