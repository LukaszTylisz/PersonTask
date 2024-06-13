using Application.Tests.Mocks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.TestDbContext;

public class PersonDbContextInMemory : IDisposable
{
    private readonly PersonDbContext _context;
    public PersonDbContextInMemory()
    {
        var options = new DbContextOptionsBuilder<PersonDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new PersonDbContext(options);
        _context.Database.EnsureCreated();
    }
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
    
    [Fact]
    public async Task AddPerson_ShouldAddPersonToDatabase()
    {
        var persons = TestDataGenerator.GeneratePersons(10);
        
        await _context.Persons.AddRangeAsync(persons);
        await _context.SaveChangesAsync();
        
        var count = await _context.Persons.CountAsync();
        Assert.Equal(10, count);
    }
}