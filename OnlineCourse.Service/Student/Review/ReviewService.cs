using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Extensions;
using OnlineCourse.Common.Models;
using OnlineCourse.Data.Entites;
using OnlineCourse.Data.Repositories;
using OnlineCourse.Service.Helpers;
using StatusGeneric;

namespace OnlineCourse.Service.Student;
using static OnlineCourse.Common.Constants.StatusConstants;
public class ReviewService(IUnitOfWork unitOfWork, IUserHelper userHelper) : StatusGenericHandler, IReviewService
{
    private readonly Guid UserId = Guid.Parse(userHelper.GetUserId());
    public async Task<string?> CreateReviewAsync(CreateReviewModel model)
    {
        await using var transaction = unitOfWork.BeginTransaction();
        try
        {
            if (!await IsCourseExistAsync(model.CourseId)) return null;
            var newReview = model.MapToEntity<Review, CreateReviewModel>();
            newReview.CreatedAt = DateTime.UtcNow;
            newReview.CreatedUserId = UserId;
            newReview.UserId = UserId;
            await unitOfWork.ReviewRepository().AddAsync(newReview);
            await unitOfWork.SaveChangesAsync();


            var course = await GetCourseByIdAsync(model.CourseId);
            if (course == null) return null;
            course.Rating = await CalculateCourseRatingAsync(model.CourseId);
            unitOfWork.CourseRepository().Update(course);
            await unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();
            return "Sharh muvaffaqiyatli qo‘shildi";

        }
        catch
        {
            await transaction.RollbackAsync();
            return null;
        }
    }

    private async Task<bool> IsCourseExistAsync(int courseId)
    {
        var userCourseId = await unitOfWork.UserCourseRepository()
            .GetAll()
            .Where(x => x.UserId == UserId && x.CourseId == courseId && x.IsActive)
            .Select(x => x.CourseId)
            .FirstOrDefaultAsync();


        bool isExist = await unitOfWork.CourseRepository().GetAll().Where(x => x.Id == userCourseId && x.StatusId == Public).AnyAsync();

        if (!isExist)
        {
            AddError("Kurs topilmadi");
            return false;
        }

        return isExist;

    }
    private async Task<decimal?> CalculateCourseRatingAsync(int courseId)
    {

        var reviews = await unitOfWork.ReviewRepository()
            .GetAll()
            .Where(x => x.CourseId == courseId)
            .ToListAsync();
        int sum = 0;

        foreach (var review in reviews)
        {
            sum += review.Rating;
        }

        return sum / 5;
    }
    private async Task<Data.Entites.Course?> GetCourseByIdAsync(int courseId)
    {

        var userCourseId = await unitOfWork
                   .UserCourseRepository()
                   .GetAll()
                   .Where(x => x.UserId == UserId && x.IsActive && x.CourseId == courseId)
                   .Select(x => x.CourseId)
                   .FirstOrDefaultAsync();

        var course = await unitOfWork
            .CourseRepository()
            .GetAll()
            .Where(x => x.StatusId != Deleted && x.Id == userCourseId)
            .FirstOrDefaultAsync();

        if (course is null)
        {
            AddError("Kurs topilmadi. Iltimos, keyinroq qayta urinib ko‘ring.");
            return null;
        }


        return course;
    }


}
