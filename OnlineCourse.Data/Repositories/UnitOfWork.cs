using Microsoft.EntityFrameworkCore.Storage;
using OnlineCourse.Data.Contexts;
using OnlineCourse.Data.Entites;

namespace OnlineCourse.Data.Repositories;

public class UnitOfWork(
    AppDbContext context,
    IBaseRepository<Article> articleRepository,
    IBaseRepository<Category> categoryRepository,
    IBaseRepository<Comment> commentRepository,
    IBaseRepository<Contact> contactRepository,
    IBaseRepository<Content> contentRepository,
    IBaseRepository<ContentType> contentTypeRepository,
    IBaseRepository<Course> courseRepository,
    IBaseRepository<Faq> faqRepository,
    IBaseRepository<Feedback> feedbackRepository,
    IBaseRepository<Lesson> lessonRepository,
    IBaseRepository<Level> levelRepository,
    IBaseRepository<Review> reviewRepository,
    IBaseRepository<Role> roleRepository,
    IBaseRepository<Status> statusRepository,
    IBaseRepository<UserCourse> userCourseRepository,
    IBaseRepository<User> userRepository
) : IUnitOfWork
{
    public IBaseRepository<Article> ArticleRepository() =>
        articleRepository ?? new BaseRepository<Article>(context);

    public IBaseRepository<Category> CategoryRepository() =>
        categoryRepository ?? new BaseRepository<Category>(context);

    public IBaseRepository<Comment> CommentRepository() =>
        commentRepository ?? new BaseRepository<Comment>(context);

    public IBaseRepository<Contact> ContactRepository() =>
        contactRepository ?? new BaseRepository<Contact>(context);

    public IBaseRepository<Content> ContentRepository() =>
        contentRepository ?? new BaseRepository<Content>(context);

    public IBaseRepository<ContentType> ContentTypeRepository() =>
        contentTypeRepository ?? new BaseRepository<ContentType>(context);

    public IBaseRepository<Course> CourseRepository() =>
        courseRepository ?? new BaseRepository<Course>(context);

    public IBaseRepository<Faq> FaqRepository() =>
        faqRepository ?? new BaseRepository<Faq>(context);

    public IBaseRepository<Feedback> FeedbackRepository() =>
        feedbackRepository ?? new BaseRepository<Feedback>(context);

    public IBaseRepository<Lesson> LessonRepository() =>
        lessonRepository ?? new BaseRepository<Lesson>(context);

    public IBaseRepository<Level> LevelRepository() =>
        levelRepository ?? new BaseRepository<Level>(context);

    public IBaseRepository<Review> ReviewRepository() =>
        reviewRepository ?? new BaseRepository<Review>(context);

    public IBaseRepository<Role> RoleRepository() =>
        roleRepository ?? new BaseRepository<Role>(context);

    public IBaseRepository<Status> StatusRepository() =>
        statusRepository ?? new BaseRepository<Status>(context);

    public IBaseRepository<UserCourse> UserCourseRepository() =>
        userCourseRepository ?? new BaseRepository<UserCourse>(context);

    public IBaseRepository<User> UserRepository() =>
        userRepository ?? new BaseRepository<User>(context);

    public IDbContextTransaction BeginTransaction() =>
        context.Database.BeginTransaction();
}

