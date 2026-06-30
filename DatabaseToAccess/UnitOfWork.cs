using Microsoft.EntityFrameworkCore.Storage;
using Service.Application.Interfaces;

namespace DatabaseToAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork (BaseDbContext context)
        {
            _context = context;
        }
        public async Task BeginTransactionAsync(CancellationToken token)
        {
            if (_transaction != null) return; //to do make exception

            _transaction = await _context.Database.BeginTransactionAsync(token);
        }

        public async Task CommitAsync(CancellationToken token)
        {
            if (_transaction == null) return;
            
            try 
            {
                await _transaction.CommitAsync(token);
            }
            finally 
            {
                await CleanupAsync();
            }
        }

        public async Task RollbackAsync(CancellationToken token)
        {
            if (_transaction == null) return;

            try 
            {
                await _transaction.RollbackAsync(token);
            }
            finally 
            {
                await CleanupAsync();
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken token)
        {
            return _context.SaveChangesAsync(token);
        }

        private async Task CleanupAsync()
        {
            if (_transaction == null) return;

            await _transaction.DisposeAsync();
            _transaction = null;

        }
    }
}