namespace OnlineCourse.Common.Models;

public class CreateUserCourseModel : CreateBaseDateTimeModel
{
    public Guid UserId { get; set; }
    public int CourseId { get; set; }
    public bool IsActive { get; set; } = false;
}