using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Common.Models;

public class CreateContentTypeModel : CreateBaseInfoModel
{
    [Required(ErrorMessage = "Tur nomi kiritilishi shart")]
    public string TypeName { get; set; } = string.Empty;
}
