namespace Service.Application.Interfaces {
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync(CancellationToken token);
        Task<int> SaveChangesAsync(CancellationToken token);
        Task CommitAsync(CancellationToken token);
        Task RollbackAsync(CancellationToken token);
    }
}