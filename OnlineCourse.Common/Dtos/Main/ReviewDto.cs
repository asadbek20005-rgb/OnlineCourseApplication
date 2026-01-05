namespace OnlineCourse.Common.Dtos;

public class ReviewDto : BaseDateTimeDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int CourseId { get; set; }

    // Main
    public string Message { get; set; } = string.Empty;
    public int Rating { get; set; } // 1-5 
}
