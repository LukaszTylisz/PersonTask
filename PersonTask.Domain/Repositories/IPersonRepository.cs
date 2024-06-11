using PersonTask.Domain.Entities;

namespace PersonTask.Domain.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);
    Task<int> Create(Person entity);
    Task Delete(Person entity);
    Task SaveChanges();
}