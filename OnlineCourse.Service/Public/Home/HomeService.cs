using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Data.Repositories;

namespace OnlineCourse.Service.Public.Home;
using static OnlineCourse.Common.Constants.StatusConstants;
public class HomeService(IUnitOfWork unitOfWork) : IHomeService
{
    public async Task<IEnumerable<CourseDto>> GetFeateredCourses()
    {
        var query = unitOfWork.CourseRepository().GetAll().OrderBy(x => x).Take(10).Where(x => x.StatusId == Public);
        var config = GetCustomConfig();
        var result = query.MapToDtos<Data.Entites.Course, CourseDto>(config);
        return await result.ToListAsync();
    }

    public async Task<IEnumerable<CategoryDto>> GetTopCategories()
    {
        var query = unitOfWork.CategoryRepository().GetAll();
        var result = query.MapToDtos<Data.Entites.Category, CategoryDto>();
        return await result.ToListAsync();
    }


    private TypeAdapterConfig GetCustomConfig()
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<Data.Entites.Course, CourseDto>()
            .Map(x => x.StatusName, x => x.Status.FullName)
            .Map(x => x.LevelName, x => x.Level.FullName)
            .Map(x => x.CategoryName, x => x.Category.FullName)
            .Map(x => x.PhotoContentUrl, x => x.Content.Url);

        return config;

    }

}