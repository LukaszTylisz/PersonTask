using Application.Contracts.Persistence;
using Domain.Entitites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PersonRepository(PersonDbContext context) : GenericRepository<Person>(context), IPersonRepository
{
    public async Task<IReadOnlyList<Person>> GetAllWithEmails()
    {
        return await context
            .Persons
            .Include(p => p.Emails)
            .ToListAsync();
    }

    public async Task<Person?> GetByIdWithEmails(int id)
    {
        return await context
            .Persons
            .Include(p => p.Emails)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}