namespace EducationPortal.DAL.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataAccessLayer.Entities;
    using EducationPortal.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
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
}
