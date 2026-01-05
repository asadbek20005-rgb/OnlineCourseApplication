namespace OnlineCourse.Common.Dtos;

public class UserDto : BaseDateTimeDto
{
    // Primary and Foreign Keys
    public Guid Id { get; set; }
    public int StatusId { get; set; }
    public int RoleId { get; set; }
    public int PhotoContentId { get; set; }

    // Main Properties
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string Gender { get; set; } = string.Empty; // Default: NotSpecified
}
