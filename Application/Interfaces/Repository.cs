using Domain;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface Repository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);

        Task<T?> GetByIdAsync(Guid id);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
