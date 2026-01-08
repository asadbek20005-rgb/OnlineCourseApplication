using Mapster;

namespace OnlineCourse.Common.Extensions;

public static class MapToExtension
{
    public static TEntity MapToEntity<TEntity, TModel>(this TModel model, TypeAdapterConfig? config = null)
    {
        var entity = config is null ? model.Adapt<TEntity>()
            : model.Adapt<TEntity>(config);
        return entity;
    }

    public static List<TEntity> MapToEntities<TModel, TEntity>(this List<TModel> models, TypeAdapterConfig? config = null)
    {
        var entites = config is null ? models.Adapt<List<TEntity>>()
            : models.Adapt<List<TEntity>>(config);
        return entites;
    }

    public static TDto MapToDto<TEntity, TDto>(
        this TEntity source,
        TypeAdapterConfig? config = null)
    {
        return config == null
            ? source.Adapt<TDto>()                 // use default global config
            : source.Adapt<TDto>(config);          // use custom config
    }

    public static List<TDto> MapToDtos<TEntity, TDto>(this List<TEntity>? entities)
    {
        if (entities is null)
            return new();

        return entities.Select(entity => entity.MapToDto<TEntity, TDto>()).ToList();
    }


    // IQueryable<Entity> -> IQueryable<DTO>
    public static IQueryable<TDto> MapToDtos<TEntity, TDto>(
        this IQueryable<TEntity> query,
        TypeAdapterConfig? config = null)
    {
        return config == null
            ? query.ProjectToType<TDto>()                 // default config
            : query.ProjectToType<TDto>(config);          // custom config
    }

    public static TEntity MapForUpdate<TEntity, TModel>(this TModel model, TEntity entity)
    {
        var entityProperties = typeof(TEntity).GetProperties();
        var modelProperties = typeof(TModel).GetProperties();

        foreach (var modelProperty in modelProperties)
        {
            var entityProperty = entityProperties.FirstOrDefault(p => p.Name == modelProperty.Name);
            if (entityProperty is null)
                continue;

            // ? Skip lists / collections
            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(entityProperty.PropertyType)
                && entityProperty.PropertyType != typeof(string))
                continue;

            // ? Skip classes except string
            if (entityProperty.PropertyType.IsClass && entityProperty.PropertyType != typeof(string))
                continue;

            var newValue = modelProperty.GetValue(model);
            var oldValue = entityProperty.GetValue(entity);

            // faqat null bo?lmagan va eski qiymatdan farq qiladiganlarini update qilamiz
            if (newValue is not null && !Equals(newValue, oldValue))
            {
                entityProperty.SetValue(entity, newValue);
            }
        }

        return entity;
    }

    public static TEntity MapToUpdate<TEntity, TModel>(this TModel model, TEntity entity, TypeAdapterConfig? config = null)
    {
        return config != null ? model.Adapt(entity, config) : model.Adapt(entity);
    }
}