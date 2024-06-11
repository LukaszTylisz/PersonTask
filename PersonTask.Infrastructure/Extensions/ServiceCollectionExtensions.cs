using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonTask.Domain.Entities;
using PersonTask.Domain.Repositories;
using PersonTask.Infrastructure.Persistence;
using PersonTask.Infrastructure.Repositories;
using PersonTask.Infrastructure.Seeders;

namespace PersonTask.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PersonsDb");
        services.AddDbContext<PersonsDbContext>(options =>
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<PersonsDbContext>();

        services
            .AddScoped<IPersonSeeder, PersonSeeder>()
            .AddScoped<IPersonRepository, PersonsRepository>()
            .AddScoped<IEmailsRepository, EmailsRepository>();
    }
}