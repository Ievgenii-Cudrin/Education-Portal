using DataAccessLayer.Entities;
using EducationPortal.DAL.DataContext.EntitiesConfiguration;
using EducationPortal.Domain.Entities;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.DAL.DataContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Skill> Skills { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseSkill> CourseSkills { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserSkill> UserSkills { get; set; }

        public DbSet<UserCourse> UserCourses { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<UserMaterial> UserMaterials { get; set; }

        public DbSet<CourseMaterial> CourseMaterials { get; set; }

        public DbSet<UserCourseMaterial> UserCourseMaterials { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-7QBD7T4;Database=EducationPortalU;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseSkillConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserSkillConfiguration());
            modelBuilder.ApplyConfiguration(new UserCourseConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new UserMaterialConfiguration());
            modelBuilder.ApplyConfiguration(new CourseMaterialConfiguration());
            modelBuilder.ApplyConfiguration(new UserCourseMaterialConfiguration());

            modelBuilder.Entity<Article>().ToTable("Articles");
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Video>().ToTable("Videos");
        }
    }
}
