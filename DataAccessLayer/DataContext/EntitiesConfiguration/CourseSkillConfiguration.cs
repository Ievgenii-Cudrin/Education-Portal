﻿using DataAccessLayer.Entities;
using EducationPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPortal.DAL.DataContext
{
    public class CourseSkillConfiguration : IEntityTypeConfiguration<CourseSkill>
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
}
