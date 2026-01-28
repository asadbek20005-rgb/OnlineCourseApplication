namespace OnlineCourse.Common.Dtos;

public class UserDto : BaseDateTimeDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string Gender { get; set; } = string.Empty; // Default: NotSpecified

    public string? PhotoUrl { get; set; } 
}
    