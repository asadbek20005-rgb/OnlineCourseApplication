using OnlineCourse.Common.Extensions;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Helpers;
using StatusGeneric;

namespace OnlineCourse.Service.Admin.Child;
public class BaseInfoService<TEntity>(IBaseRepository<TEntity> baseRepository, IUserHelper userHelper) : StatusGenericHandler, IBaseInfoService<TEntity> where TEntity : class
{
    private Guid UserId => Guid.Parse(userHelper.GetUserId());
    public List<TDto> GetAll<TDto>()
    {
        var entities = baseRepository.GetAll().ToList();

        return entities.MapToDtos<TEntity, TDto>();
    }

    public async Task<TDto?> GetByIdAsync<TDto, TId>(TId id)
    {
        var (check, entity) = await GetEntityIfExist(id);
        if (!check)
            return default;

        return entity!.MapToDto<TEntity, TDto>();
    }

    public async Task<string> CreateAsync<TModel>(TModel model)
    {
        var entity = model.MapToEntity<TEntity, TModel>();

        var createdAt = typeof(TEntity).GetProperty("CreatedAt");
        var createdUserId = typeof(TEntity).GetProperty("CreatedUserId");
      

        if (createdAt != null && createdAt.PropertyType == typeof(DateTime) && createdAt.CanWrite)
        {
            createdAt.SetValue(entity, DateTime.UtcNow);
        }

        if (createdUserId != null && createdUserId.PropertyType == typeof(Guid?) && createdUserId.CanWrite)
        {
            createdUserId.SetValue(entity, UserId);
        }
        await baseRepository.AddAsync(entity);
        await baseRepository.SaveChangesAsync();
        return "Added successfully";
    }

    public async Task<string?> UpdateAsync<TId, TModel>(TId id, TModel model)
    {
        var (check, entity) = await GetEntityIfExist(id);
        if (!check)
            return null;

        entity = model.MapForUpdate(entity);
        var updatedAt = typeof(TEntity).GetProperty("UpdatedAt");
        var updatedUserId = typeof(TEntity).GetProperty("UpdatedUserId");

        if (updatedAt != null && updatedAt.PropertyType == typeof(DateTime?) && updatedAt.CanWrite)
        {
            updatedAt.SetValue(entity, DateTime.UtcNow);
        }

        if (updatedUserId != null && updatedUserId.PropertyType == typeof(Guid?) && updatedUserId.CanWrite)
        {
            updatedUserId.SetValue(entity, UserId);
        }


        baseRepository.Update(entity!);
        await baseRepository.SaveChangesAsync();

        return "Updated successfully";
    }

    public async Task<string?> DeleteByIdAsync<TId>(TId id)
    {
        var (check, entity) = await GetEntityIfExist(id);
        if (!check)
            return null;


        baseRepository.Delete(entity!);
        await baseRepository.SaveChangesAsync();
        return "Deleted successfully";
    }

    private async Task<Tuple<bool, TEntity?>> GetEntityIfExist<TId>(TId id)
    {
        var entity = await baseRepository.GetByIdAsync(id);

        if (entity is null)
        {
            AddError("Entity not found");
            return new(false, null);
        }

        return new(true, entity);
    }
}
