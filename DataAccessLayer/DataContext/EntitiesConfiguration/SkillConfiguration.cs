namespace EducationPortal.DAL.DataContext.EntitiesConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataAccessLayer.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("Skills").HasKey(s => s.Id);
        }
    }
}
