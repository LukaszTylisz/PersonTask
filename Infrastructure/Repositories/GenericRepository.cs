using Application.Contracts.Persistence;
using Domain.Common;
using Domain.Entitites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T>(PersonDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    public async Task Create(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Set<T>().Update(entity);
    }

    public async Task Delete(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }
    public async Task<IReadOnlyList<T>> GetAll()
    {
        return await context
            .Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await context
            .Set<T>()
            .FindAsync(id);
    }

   
}