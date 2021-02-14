using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.DAL.SQL.DataContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-7QBD7T4;Database=EducationPortal;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());

        }

        public class SkillConfiguration : IEntityTypeConfiguration<Skill>
        {
            public void Configure(EntityTypeBuilder<Skill> builder)
            {
                builder.ToTable("Skills").HasKey(s => s.Id);
            }
        }

        public class CourseConfiguration : IEntityTypeConfiguration<Course>
        {
            public void Configure(EntityTypeBuilder<Course> builder)
            {
                builder.ToTable("Courses").HasKey(s => s.Id);
            }
        }
    }
}
