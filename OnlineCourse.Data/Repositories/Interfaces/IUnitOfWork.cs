using Microsoft.EntityFrameworkCore.Storage;
using OnlineCourse.Data.Entites;

namespace OnlineCourse.Data.Repositories;

public interface IUnitOfWork
{
    IBaseRepository<Role> RoleRepository();
    IBaseRepository<Category> CategoryRepository();
    IBaseRepository<ContentType> ContentTypeRepository();
    IBaseRepository<Level> LevelRepository();
    IBaseRepository<Status> StatusRepository();

    IBaseRepository<Article> ArticleRepository();
    IBaseRepository<Comment> CommentRepository();
    IBaseRepository<Contact> ContactRepository();
    IBaseRepository<Content> ContentRepository();
    IBaseRepository<Course> CourseRepository();
    IBaseRepository<Faq> FaqRepository();
    IBaseRepository<Feedback> FeedbackRepository();
    IBaseRepository<Lesson> LessonRepository();
    IBaseRepository<Review> ReviewRepository();
    IBaseRepository<User> UserRepository();
    IBaseRepository<UserCourse> UserCourseRepository();

    IDbContextTransaction BeginTransaction();

    Task SaveChangesAsync();


}
