using PersonTask.Domain.Entities;
using PersonTask.Domain.Constants;

namespace PersonTask.Domain.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);
    Task<int> Create(Person entity);
    Task Delete(Person entity);
    Task SaveChanges();
    Task<(IEnumerable<Person>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
}