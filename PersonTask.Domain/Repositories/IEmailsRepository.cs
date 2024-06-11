using PersonTask.Domain.Entities;

namespace PersonTask.Domain.Repositories;

public interface IEmailsRepository
{
    Task<int> Create(PersonEmail entity);
    Task Delete(IEnumerable<PersonEmail> entities);
}