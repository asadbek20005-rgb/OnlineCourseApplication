using System.Linq.Expressions;

namespace OnlineCourse.Data.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> GetByIdAsync<TId>(TId id);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync();

    Task AddRangeAsync(IQueryable<TEntity> entities);

}