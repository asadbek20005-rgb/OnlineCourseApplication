using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourse.Data.Entites;

public abstract class BaseDateTime
{
    [Column("created_at")]
    [Required]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("created_user_id")]
    [Required]
    public Guid CreatedUserId { get; set; }

    [Column("updated_user_id")]
    public Guid? UpdatedUserId { get; set; }

}