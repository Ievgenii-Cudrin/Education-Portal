namespace EducationPortal.DAL.DataContext
{
    using DataAccessLayer.Entities;
    using EducationPortal.Domain.Entities;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-7QBD7T4;Database=EducationPortal;Trusted_Connection=True;");
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

        class SkillConfiguration : IEntityTypeConfiguration<Skill>
        {
            public void Configure(EntityTypeBuilder<Skill> builder)
            {
                builder.ToTable("Skills").HasKey(s => s.Id);

            }
        }

        class CourseConfiguration : IEntityTypeConfiguration<Course>
        {
            public void Configure(EntityTypeBuilder<Course> builder)
            {
                builder.ToTable("Courses").HasKey(s => s.Id);
            }
        }

        class CourseSkillConfiguration : IEntityTypeConfiguration<CourseSkill>
        {
            public void Configure(EntityTypeBuilder<CourseSkill> builder)
            {
                builder.ToTable("CourseSkills").HasKey(s => new { s.CourseId, s.SkillId });

                builder.HasOne<Course>(c => c.Course)
                    .WithMany(s => s.CourseSkills)
                    .HasForeignKey(s => s.CourseId);

                builder.HasOne<Skill>(s => s.Skill)
                    .WithMany(s => s.CourseSkills)
                    .HasForeignKey(s => s.SkillId);
            }
        }

        class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.ToTable("Users").HasKey(s => s.Id);
            }
        }

        class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
        {
            public void Configure(EntityTypeBuilder<UserSkill> builder)
            {
                builder.ToTable("UserSkills").HasKey(s => new { s.UserId, s.SkillId });

                builder.HasOne<User>(c => c.User)
                    .WithMany(s => s.UserSkills)
                    .HasForeignKey(s => s.UserId);

                builder.HasOne<Skill>(s => s.Skill)
                    .WithMany(s => s.UserSkills)
                    .HasForeignKey(s => s.SkillId);
            }
        }

        class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
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

        class MaterialConfiguration : IEntityTypeConfiguration<Material>
        {
            public void Configure(EntityTypeBuilder<Material> builder)
            {
                builder.ToTable("Materials").HasKey(s => s.Id);
            }
        }

        class UserMaterialConfiguration : IEntityTypeConfiguration<UserMaterial>
        {
            public void Configure(EntityTypeBuilder<UserMaterial> builder)
            {
                builder.ToTable("UserMaterials").HasKey(s => new { s.UserId, s.MaterialId });

                builder.HasOne<User>(c => c.User)
                    .WithMany(s => s.UserMaterials)
                    .HasForeignKey(s => s.UserId);

                builder.HasOne<Material>(s => s.Material)
                    .WithMany(s => s.UserMaterials)
                    .HasForeignKey(s => s.MaterialId);
            }
        }

        class CourseMaterialConfiguration : IEntityTypeConfiguration<CourseMaterial>
        {
            public void Configure(EntityTypeBuilder<CourseMaterial> builder)
            {
                builder.ToTable("CourseMaterials").HasKey(s => new { s.CourseId, s.MaterialId });

                builder.HasOne<Course>(c => c.Course)
                    .WithMany(s => s.CourseMaterials)
                    .HasForeignKey(s => s.CourseId);

                builder.HasOne<Material>(s => s.Material)
                    .WithMany(s => s.CourseMaterials)
                    .HasForeignKey(s => s.MaterialId);
            }
        }

        class UserCourseMaterialConfiguration : IEntityTypeConfiguration<UserCourseMaterial>
        {
            public void Configure(EntityTypeBuilder<UserCourseMaterial> builder)
            {
                builder.ToTable("UserCourseMaterials").HasKey(s => new { s.UserCourseId, s.MaterialId });

                builder.HasOne<UserCourse>(c => c.UserCourse)
                    .WithMany(s => s.UserCourseMaterials)
                    .HasForeignKey(s => s.UserCourseId)
                    .HasPrincipalKey(s => s.Id);

                builder.HasOne<Material>(s => s.Material)
                    .WithMany(s => s.UserCourseMaterials)
                    .HasForeignKey(s => s.MaterialId);
            }
        }
    }
}
