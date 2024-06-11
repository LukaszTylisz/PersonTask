using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonTask.Domain.Entities;

namespace PersonTask.Infrastructure.Persistence;

internal class PersonsDbContext(DbContextOptions<PersonsDbContext> options)
    : IdentityDbContext(options)
{
    internal DbSet<Person> Persons { get; set; }
    internal DbSet<PersonEmail> Emails { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>()
            .HasMany(r => r.Emails)
            .WithOne()
            .HasForeignKey(d => d.PersonId);
    }
}