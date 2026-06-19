using Business.Data.Models;

namespace Service.Application.Interfaces 
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetWithIngredientsAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
