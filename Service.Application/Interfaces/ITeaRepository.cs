using Business.Data.Models;

namespace Service.Application.Interfaces 
{
    public interface ITeaRepository : IRepository<Tea>
    {
        Task<IList<Tea>> GetWithIngredientsAsync(CancellationToken cancellationToken = default);
    }
}
