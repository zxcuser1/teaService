using Business.Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.Application.Interfaces;

namespace DatabaseToAccess.Repository
{
    public class TeaRepository : Repository<Tea>, ITeaRepository
    {
        public TeaRepository(BaseDbContext context) : base(context) {}

        public async Task<IList<Tea>> GetWithIngredientsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                            .Include(t => t.TeaIngredients)
                            .ThenInclude(ti => ti.Ingredient)
                            .ToListAsync(cancellationToken);
        }
    }
}