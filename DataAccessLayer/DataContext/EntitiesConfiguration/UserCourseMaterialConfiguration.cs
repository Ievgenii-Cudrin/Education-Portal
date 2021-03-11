﻿using EducationPortal.Domain.Entities;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPortal.DAL.DataContext.EntitiesConfiguration
{
    public class UserCourseMaterialConfiguration : IEntityTypeConfiguration<UserCourseMaterial>
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
