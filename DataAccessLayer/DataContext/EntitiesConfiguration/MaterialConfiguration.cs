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

        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles").HasKey(s => s.Id);
        }

        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books").HasKey(s => s.Id);
        }

        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("Videos").HasKey(s => s.Id);
        }
    }
}
