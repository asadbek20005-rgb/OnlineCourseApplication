namespace OnlineCourse.Common.Dtos;

public class CourseDto : BaseDateTimeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Curriculum { get; set; } = null!;
    public string StatusName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string PhotoContentUrl { get; set; } = null!;
    public string LevelName { get; set; } = null!;
}
