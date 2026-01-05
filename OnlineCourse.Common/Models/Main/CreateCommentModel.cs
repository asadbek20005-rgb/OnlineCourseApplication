using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateCommentModel : CreateBaseDateTimeModel
{
    // Keys
    public int CommentReplyId { get; set; }

    [Required(ErrorMessage = "Maqola tanlanishi shart.")]
    public int ArticleId { get; set; }

    [Required(ErrorMessage = "Kurs tanlanishi shart.")]
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Kontakt tanlanishi shart.")]
    public int ContactId { get; set; }

    // Mains
    [Required(ErrorMessage = "Email manzil kiritilishi shart.")]
    [EmailAddress(ErrorMessage = "Email manzil noto‘g‘ri formatda.")]
    [StringLength(100, ErrorMessage = "Email manzil 100 ta belgidan oshmasligi kerak.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ism kiritilishi shart.")]
    [StringLength(150, ErrorMessage = "Ism 150 ta belgidan oshmasligi kerak.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Izoh matni kiritilishi shart.")]
    public string Text { get; set; } = string.Empty;
}
