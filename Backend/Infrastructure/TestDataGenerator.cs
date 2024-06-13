using Bogus;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class TestDataGenerator
{
    public static async Task InitialiseAndSeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<GenerateFakeData>();
        await initialiser.InitialiseAndSeedAsync();
    }
}

internal class GenerateFakeData
{
    private readonly PersonDbContext _context;
    private readonly ILogger<GenerateFakeData> _logger;

    public GenerateFakeData(PersonDbContext context, ILogger<GenerateFakeData> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAndSeedAsync()
    {
        try
        {
            _logger.LogInformation("Migrating and seeding database...");

            await _context.Database.MigrateAsync();
            await TrySeedAsync();

            _logger.LogInformation("Database migrated and seeded.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating and seeding the database.");
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
            _logger.LogInformation("Seeding persons data...");

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
                })
                .Generate(100);

            await _context.AddRangeAsync(personsGenerator);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Data seeded successfully.");
        }
        else
        {
            _logger.LogInformation("Persons already exist in the database. No data seeding needed.");
        }
    }
}