using PersonTask.API.Extensions;
using PersonTask.API.Middlewares;
using PersonTask.Application.Extensions;
using PersonTask.Infrastructure.Extensions;
using PersonTask.Infrastructure.Seeders;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);


    var app = builder.Build();

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IPersonSeeder>();

 //   await seeder.Seed();
    // Configure the HTTP request pipeline.
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeLoggingMiddleware>();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseHttpsRedirection();

    // app.MapGroup("api/identity")
    //     .WithTags("Identity")
    //     .MapIdentityApi<User>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}


namespace PersonTask.API
{
    public partial class Program { }
}