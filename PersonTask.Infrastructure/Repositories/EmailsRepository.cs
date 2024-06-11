using PersonTask.Domain.Entities;
using PersonTask.Domain.Repositories;
using PersonTask.Infrastructure.Persistence;

namespace PersonTask.Infrastructure.Repositories;

internal class EmailsRepository(PersonsDbContext dbContext) : IEmailsRepository
{
    public async Task<int> Create(PersonEmail entity)
    {
        dbContext.Emails.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(IEnumerable<PersonEmail> entities)
    {
       dbContext.Emails.RemoveRange(entities);
       await dbContext.SaveChangesAsync();
    }
}