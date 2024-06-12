using Domain.Entitites;

namespace Application.Contracts.Persistence;

public interface IPersonRepository : IGenericRepository<Person>
{
    Task<IReadOnlyList<Person>> GetAllWithEmails();
    Task<Person?> GetByIdWithEmails(int id);
}