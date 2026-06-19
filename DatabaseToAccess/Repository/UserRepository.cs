using System.Threading.Tasks;
using Business.Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.Application.Interfaces;

namespace DatabaseToAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context) {}

        public async Task<User?> GetWithIngredientsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                            .Include(u => u.UserIngredients)
                            .ThenInclude(ui => ui.Ingredient)
                            .FirstOrDefaultAsync(u => u.Guid == userId, cancellationToken);
        }
    }
}