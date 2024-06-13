using Application.Contracts.Persistence;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContext<PersonDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PersonDatabaseConnection"),
                b => b.MigrationsAssembly(typeof(PersonDbContext).Assembly.FullName)))
            .AddScoped<IPersonRepository, PersonRepository>()
            .AddScoped<GenerateFakeData>();

        return services;
    }
}