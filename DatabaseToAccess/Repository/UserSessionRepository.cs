using Business.Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.Application.Interfaces;

namespace DatabaseToAccess.Repository
{
    public class UserSessionRepository : Repository<UserSession>, IUserSessionRepository
    {
        public UserSessionRepository (BaseDbContext context) : base(context) {}

        public async Task<UserSession?> GetUserSessionAsync(Guid userId, string deviceId, CancellationToken token = default)
        {
            return await _dbSet.FirstOrDefaultAsync
            (
                s => s.UserId == userId &&
                s.DeviceId == deviceId &&
                s.ClosedAt == null,
                token
            );
        }
    }
}