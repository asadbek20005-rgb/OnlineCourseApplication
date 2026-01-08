using StatusGeneric;

namespace OnlineCourse.Service.Admin;

public interface IBaseInfoService<TEntity> : IStatusGeneric
{
    List<TDto> GetAll<TDto>();
    Task<TDto?> GetByIdAsync<TDto, TId>(TId id);
    Task<string> CreateAsync<TModel>(TModel model);
    Task<string?> UpdateAsync<TId, TModel>(TId id, TModel model);
    Task<string?> DeleteByIdAsync<TId>(TId id);
}
