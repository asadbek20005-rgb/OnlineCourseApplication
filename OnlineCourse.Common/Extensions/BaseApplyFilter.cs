using OnlineCourse.Common.FilterOptions;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace OnlineCourse.Common.Extensions;

public static class BaseApplyFilter
{
    public static (IQueryable<TEntity>, int totalCount) ApplyBaseFilter<TEntity>(
        this IQueryable<TEntity> query,
        BaseFilterOptions options,
        Expression<Func<TEntity, bool>>? searchExpression = null
        )
    {
        // Search
        if (options.HasSearch() && searchExpression is not null)
        {
            query = query.Where(searchExpression);
        }


        var totalCount = query.Count();

        // Sort

        query = query.OrderBy(options.HasSort()
            ? $"{options.SortBy} {options.OrderType}"
            : $"Id {BaseFilterOptions.ORDER_TYPE_DESC}");

        // Pagination

        query = query
            .Skip((options.Page - 1) * options.PageSize)
            .Take(options.PageSize);

        return (query, totalCount);
    }
}
