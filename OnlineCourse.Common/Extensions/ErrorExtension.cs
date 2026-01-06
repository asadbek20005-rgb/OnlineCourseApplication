using Microsoft.AspNetCore.Mvc.ModelBinding;
using StatusGeneric;

namespace OnlineCourse.Common.Extensions;

public static class ErrorExtension
{
    public static void CopyToModelState(this IStatusGeneric status, ModelStateDictionary modelState)
    {
        if (!status.HasErrors)
        {
            return;
        }

        foreach (ErrorGeneric error in status.Errors)
        {
            var ll = error.ErrorResult.MemberNames.FirstOrDefault();
            modelState.AddModelError(
                key: error.ErrorResult.MemberNames.Count() == 1 ? error.ErrorResult.MemberNames.First() : "",
                error.ToString());
        }
    }

    public static ErrorResponse ToErrorResponse(this IStatusGeneric status)
    {
        var response = new ErrorResponse();

        if (!status.HasErrors)
            return response;

        foreach (var error in status.Errors)
        {
            response.Errors.Add(error.ToString());
        }

        return response;
    }
}

public class ErrorResponse
{
    public bool Success => false;
    public List<string> Errors { get; set; } = new();
}
