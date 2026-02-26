using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Providers
{
    internal class RepositoryImp<T> : Repository<T> where T : BaseEntity
    {
        private readonly DataContext _dataContext;
        private readonly DbSet<T> _dbSet;
        public RepositoryImp(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = dataContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}
