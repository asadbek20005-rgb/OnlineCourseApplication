using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;

namespace OnlineCourse.Service.Instructor;

public static class LessonListSortFilter
{
    public static (IQueryable<Data.Entites.Lesson>, int totalCount) ApplyFilter(
        this IQueryable<Data.Entites.Lesson> query,
        LessonFilterOptions options)
    {
        if (options is null)
            return (query, query.Count());

        // ?? Status filter
        if (options.StatusId.HasValue)
        {
            query = query.Where(x => x.StatusId == options.StatusId.Value);
        }

        // ?? Course filter
        if (options.CourseId.HasValue)
        {
            query = query.Where(x => x.CourseId == options.CourseId.Value);
        }



        (query, int totalCount) = query.ApplyBaseFilter(
          options
      );

        return (query, totalCount);
    }
}
