using Business.Data.Models;
using Business.Data.Enums;
using Service.Application.AuthService.Dto;
using Service.Application.Interfaces;
using Service.Application.TokenService;

namespace Service.Application.AuthService
{
    public class AuthService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<RefreshToken> _tokenRepo;
        private readonly IRepository<UserSession> _sessionRepo;
        private readonly IRepository<Role> _roleRepo;
        private readonly TokenHelper _tokenHelper;
        public AuthService
        (
            IRepository<User> userRepo,
            IRepository<RefreshToken> tokenRepo,
            IRepository<UserSession> sessionRepo,
            IRepository<Role> roleRepo,
            TokenHelper tokenHelper 
        )
        {
            _userRepo = userRepo;
            _tokenRepo = tokenRepo;
            _sessionRepo = sessionRepo;
            _roleRepo = roleRepo;
            _tokenHelper = tokenHelper;
        }

        public async Task<TokenDto> Register
        (
            string nickName,
            string email,
            string password,
            string deviceId,
            CancellationToken token
        )
        {
            var roleId = (await _roleRepo.GetAllAsync()).FirstOrDefault(r => r.UserRole == Roles.User)!.Guid;

            var user = new User
            {
                NickName = nickName,
                Email = email,
                Password = TokenHelper.CreateSHA512(password),
                RoleId = roleId
            };

            await _userRepo.AddAsync(user, token);

            await _userRepo.SaveChangesAsync(token);

            return new TokenDto
            {
                AccessToken = _tokenHelper.GenerateAccessToken(user),
                RefreshToken = await _tokenHelper.GenerateRefreshTokenAsync(user.Guid, deviceId, token)
            };
        }
    }
}