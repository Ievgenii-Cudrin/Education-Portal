namespace EducationPortal.DAL.DataContext.EntitiesConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials").HasKey(s => s.Id);
        }
    }
}
