using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.FilterOptions;

namespace OnlineCourse.Service.Instructor;

public static class CourseListSortFilter
{
    public static (IQueryable<Data.Entites.Course>, int totalCount) ApplyFilter(
        this IQueryable<Data.Entites.Course> query,
        CourseFilterOptions options)
    {
        if (options is null)
            return (query, query.Count());

        // ?? Status filter
        if (options.StatusId.HasValue)
        {
            query = query.Where(x => x.StatusId == options.StatusId.Value);
        }

        // ?? Category filter
        if (options.CategoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == options.CategoryId.Value);
        }

        // ?? Level filter
        if (options.LevelId.HasValue)
        {
            query = query.Where(x => x.LevelId == options.LevelId.Value);
        }

        // ?? Title search (LIKE %title%)
        if (!string.IsNullOrWhiteSpace(options.Title))
        {
            var title = options.Title.Trim();

            query = query.Where(x =>
                x.Title.Contains(title)
            );
            // agar Postgres bo‘lsa:
            // EF.Functions.ILike(x.Title, $"%{title}%");
        }


        (query, int totalCount) = query.ApplyBaseFilter(
          options
      );

        return (query, totalCount);
    }
}
