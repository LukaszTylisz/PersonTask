using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonTask.Domain.Repositories;
using PersonTask.Infrastructure.Persistence;
using PersonTask.Infrastructure.Repositories;

namespace PersonTask.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PersonsDb");
        services.AddDbContext<PersonsDbContext>(options => 
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        services
            .AddScoped<IPersonRepository, PersonsRepository>()
            .AddScoped<IEmailsRepository, EmailsRepository>();
    }
}