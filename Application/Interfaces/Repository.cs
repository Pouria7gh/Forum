using Domain;

namespace Application.Interfaces
{
    public interface Repository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
    }
}
