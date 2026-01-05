using Microsoft.EntityFrameworkCore;
using OnlineCourse.Data.Entites;

namespace OnlineCourse.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ContentType> ContentTypes { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<Status> Statuses { get; set; }


    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Faq> Faqs { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }

}
