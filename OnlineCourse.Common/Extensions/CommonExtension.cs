using OnlineCourse.Common.Models;

namespace OnlineCourse.Common.Extensions;

public static class CommonExtension 
{
    public static PaginationModel<T> ToPaginationModel<T>(this IQueryable<T> query, int page, int pageSize, int totalCount) where T : class
    {
        return new PaginationModel<T>()
        {
            Rows = query.AsEnumerable(),
            Page = page,
            PageSize = pageSize,
            Total = totalCount
        };
    }
}