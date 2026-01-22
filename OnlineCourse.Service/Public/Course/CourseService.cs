using Mapster;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Entites;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Instructor;
using StatusGeneric;
using System.Linq.Dynamic.Core;

namespace OnlineCourse.Service.Public;
using static OnlineCourse.Common.Constants.StatusConstants;
public class CourseService(IUnitOfWork unitOfWork) : StatusGenericHandler, ICourseService
{
    public PaginationModel<CourseDto> GetAll(CourseFilterOptions options)
    {
        var query = unitOfWork.CourseRepository().GetAll().Where(x => x.StatusId == Public);

        var (resultQuery, totalCount) = query.ApplyFilter(options);

        var config = GetCustomConfig();

        var result = resultQuery.MapToDtos<Course, CourseDto>(config)
            .ToPaginationModel(page: options.Page, pageSize: options.PageSize, totalCount: totalCount);
        return result;
    }

    public Task<CourseDto?> GetById(int courseID)
    {
        throw new NotImplementedException();
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
