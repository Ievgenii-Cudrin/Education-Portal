using DataAccessLayer.Entities;
using EducationPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPortal.DAL.DataContext.EntitiesConfiguration
{
    public class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.ToTable("UserCourses").HasKey(s => new { s.Id, s.UserId, s.CourseId });

            builder.HasOne<User>(c => c.User)
                .WithMany(s => s.UserCourses)
                .HasForeignKey(s => s.UserId);

            builder.HasOne<Course>(s => s.Course)
                .WithMany(s => s.CourseUsers)
                .HasForeignKey(s => s.CourseId);
        }
    }
}
