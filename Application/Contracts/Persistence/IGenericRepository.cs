using Domain.Common;

namespace Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAll();
    Task<T?> GetById(int id);
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    
}