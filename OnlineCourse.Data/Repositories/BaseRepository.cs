using Microsoft.EntityFrameworkCore;
using OnlineCourse.Data.Contexts;
using System.Linq.Expressions;

namespace OnlineCourse.Data.Repositories;

public class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : class 
{
    public async Task AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();

        // Apply each Include expression dynamically
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
    public async Task<TEntity?> GetByIdAsync<TId>(TId id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        return entity;
    }

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();


    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }
}
