using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateUserModel : CreateBaseDateTimeModel
{
    // Primary and Foreign Keys
    public int StatusId { get; set; }
    public int RoleId { get; set; }
    public int PhotoContentId { get; set; }

    // Main Properties

    [StringLength(150)]
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(150)]
    [Required]
    public string LastName { get; set; } = string.Empty;

    [StringLength(30)]
    [Required]
    public string Username { get; set; } = string.Empty;

    [StringLength(100)]
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? Bio { get; set; }

    public string Gender { get; set; } = string.Empty; // Default: NotSpecified
}
