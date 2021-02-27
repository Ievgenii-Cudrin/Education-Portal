namespace EducationPortal.DAL.DataContext.EntitiesConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataAccessLayer.Entities;
    using EducationPortal.Domain.Entities;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
}
