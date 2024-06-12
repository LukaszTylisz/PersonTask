using Bogus;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class TestDataGenerator
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

internal class ApplicationDbContextInitialiser
{
    private readonly PersonDbContext _context;
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;

    public ApplicationDbContextInitialiser(PersonDbContext context, ILogger<ApplicationDbContextInitialiser> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            _logger.LogInformation("Migrating database...");
            await _context.Database.MigrateAsync();
            _logger.LogInformation("Database migrated.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (!_context.Persons.Any())
        {
            _logger.LogInformation("No persons found in the database. Seeding data...");

            var personsGenerator = new Faker<Domain.Entitites.Person>()
                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.Emails, f => new List<Domain.Entitites.Email>
                {
                new Domain.Entitites.Email
                {
                    EmailAddress = f.Internet.Email()
                }
                });

            var persons = personsGenerator.Generate(100);

            _logger.LogInformation("Generated {Count} persons.", persons.Count);

            try
            {
                await _context.AddRangeAsync(persons);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Data seeded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding data.");
            }
        }
        else
        {
            _logger.LogInformation("Persons already exist in the database. No data seeding needed.");
        }
    }
}