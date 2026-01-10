namespace OnlineCourse.Common.FilterOptions;

public class CourseFilterOptions : BaseFilterOptions
{
    public int? StatusId { get; set; }
    public int? CategoryId { get; set; }
    public int? LevelId { get; set; }
    public string? Title { get; set; } 

}
