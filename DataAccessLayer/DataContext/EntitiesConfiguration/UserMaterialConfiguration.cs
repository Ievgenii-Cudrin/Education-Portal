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

    public class UserMaterialConfiguration : IEntityTypeConfiguration<UserMaterial>
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
}
