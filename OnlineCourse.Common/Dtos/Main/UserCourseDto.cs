namespace OnlineCourse.Common.Dtos;

public class UserCourseDto : BaseDateTimeDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int CourseId { get; set; }
    public bool IsActive { get; set; } = false;
}