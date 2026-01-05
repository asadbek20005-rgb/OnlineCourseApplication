using OnlineCourse.Data.Contexts;

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

    public IQueryable<TEntity> GetAll()
    {
        return context.Set<TEntity>().AsQueryable();
    }

    public async Task<TEntity?> GetByIdAsync<TId>(TId id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        return entity;
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }
}
