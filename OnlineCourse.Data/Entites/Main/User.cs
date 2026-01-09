using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;
[Table("users")]
[Index(nameof(Username), nameof(Email), IsUnique = true)]
public class User : BaseDateTime
{
    // Primary and Foreign Keys
    [Column("id")]
    [Key]
    public Guid Id { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("photo_content_id")]
    public int? PhotoContentId { get; set; }

    // Main Properties

    [Column("first_name")]
    [StringLength(150)]
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Column("last_name")]
    [StringLength(150)]
    [Required]
    public string LastName { get; set; } = string.Empty;

    [Column("username")]
    [StringLength(30)]
    [Required]
    public string Username { get; set; } = string.Empty;

    [Column("email")]
    [StringLength(100)]
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

    [Column("password_hash")]
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("bio")]
    public string? Bio { get; set; }

    [Column("gender")]
    public string Gender { get; set; } = string.Empty; // Default: NotSpecified

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireTime { get; set; }

    // Navigation Properties

    [ForeignKey(nameof(StatusId))]
    public Status Status { get; set; } = null!;

    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; } = null!;

    [ForeignKey(nameof(PhotoContentId))]
    public Content PhotoContent { get; set; } = null!;

    [InverseProperty(nameof(UserCourse.User))]
    public List<UserCourse>? UserCourses { get; set; }

    [InverseProperty(nameof(Review.User))]
    public List<Review>? Reviews { get; set; }
    
}
