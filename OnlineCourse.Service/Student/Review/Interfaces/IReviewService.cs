using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Models;
using StatusGeneric;

namespace OnlineCourse.Service.Student;

public interface IReviewService : IStatusGeneric
{
    Task<string?> CreateReviewAsync(CreateReviewModel model);
    //Task<PaginationModel<ReviewDto>> GetCourseReviews(int courseId);
}
