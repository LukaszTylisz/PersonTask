using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PersonTask.Domain.Constants;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Repositories;
using PersonTask.Infrastructure.Persistence;

namespace PersonTask.Infrastructure.Repositories;

internal class PersonsRepository(PersonsDbContext dbContext) : IPersonRepository
{
    public async Task<int> Create(Person entity)
    {
        dbContext.Persons.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Person entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        var persons = await dbContext.Persons.ToListAsync();
        return persons;
    }

    public async Task<(IEnumerable<Person>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Persons
            .Where(r => searchPhraseLower == null || (r.FirstName.ToLower().Contains(searchPhraseLower)
                                                      || (r.LastName.ToLower().Contains(searchPhraseLower))
                                                      || r.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Person, object>>>
            {
                { nameof(Person.FirstName), r => r.FirstName },
                { nameof(Person.LastName), r => r.LastName },
                { nameof(Person.Description), r => r.Description },
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var persons = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (persons, totalCount);
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        var person = await dbContext.Persons
            .Include(r => r.Emails)
            .FirstOrDefaultAsync(x => x.Id == id);

        return person;
    }

    public Task SaveChanges()
        => dbContext.SaveChangesAsync();
}