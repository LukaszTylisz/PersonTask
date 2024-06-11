using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonTask.Domain.Entities;
using PersonTask.Infrastructure.Persistence;
using Person = PersonTask.Domain.Entities.Person;

namespace PersonTask.Infrastructure.Seeders;

internal class PersonSeeder : IPersonSeeder
{
    private readonly PersonsDbContext _dbContext;

    public PersonSeeder(PersonsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Seed()
    {
        if (_dbContext.Database.GetPendingMigrations().Any())
        {
            await _dbContext.Database.MigrateAsync();
        }

        if (await _dbContext.Database.CanConnectAsync())
        {
            if (!_dbContext.Persons.Any())
            {
                GenerateAndSeedRestaurants();
            }

            if (!_dbContext.Roles.Any())
            {
                GenerateAndSeedRoles();
            }
        }
    }

    private void GenerateAndSeedRoles()
    {
        var faker = new Faker();

        var roles = new List<IdentityRole>
        {
            new IdentityRole(faker.Random.Word()),
            new IdentityRole(faker.Random.Word()),
            new IdentityRole(faker.Random.Word())
        };

        _dbContext.Roles.AddRange(roles);
        _dbContext.SaveChanges();
    }

    private void GenerateAndSeedRestaurants()
    {
        var faker = new Faker();

        var owner = new User
        {
            Email = faker.Internet.Email(),
        };

        var persons = new List<Person>
        {
            new Person
            {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Description = faker.Lorem.Sentence(),
                Emails = new List<PersonEmail>
                {
                    new PersonEmail
                    {
                        Email = faker.Random.Words()
                    },
                    new PersonEmail
                    {
                        Email = faker.Random.Words()
                    }
                }
            }
        };
        _dbContext.Persons.AddRange(persons);
        _dbContext.SaveChanges();
    }
}