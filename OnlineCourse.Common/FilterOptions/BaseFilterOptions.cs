using OnlineCourse.Common.Extensions;

namespace OnlineCourse.Common.FilterOptions;

public abstract class BaseFilterOptions
{
    public const string ORDER_TYPE_ASC = "ASC";
    public const string ORDER_TYPE_DESC = "DESC";

    public string? Search { get; set; }
    public string? SortBy { get; set; }

    private string _order_type = ORDER_TYPE_ASC;

    public string OrderType
    {
        get => _order_type;
        set =>
            _order_type = new[] { ORDER_TYPE_ASC, ORDER_TYPE_DESC }
            .Contains(value.AsString().ToUpper()) ? value.ToUpper() : ORDER_TYPE_ASC;
    }



    private int _pageSize = 20;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 20 : value;
    }

    private int _page = 1;

    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    public bool HasSearch() => !string.IsNullOrEmpty(Search);
    public bool HasSort() => !string.IsNullOrEmpty(SortBy);
}



