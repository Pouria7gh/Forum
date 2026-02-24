using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

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
    }
}
