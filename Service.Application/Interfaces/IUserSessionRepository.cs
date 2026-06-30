using Business.Data.Models;

namespace Service.Application.Interfaces
{
    public interface IUserSessionRepository : IRepository<UserSession>
    {
        Task<UserSession?> GetUserSessionAsync(Guid userId, string deviceId, CancellationToken token = default);
    }
}