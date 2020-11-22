using System;
using System.Collections.Generic;
using System.Text;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HallOfFame.DAL.Data.Configuration.AreaPerson.AreaSkill
{
    class SkillOfLevelConfiguration :  IEntityTypeConfiguration<SkillOfLevel>
    {
        public void Configure(EntityTypeBuilder<SkillOfLevel> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(d => d.Skill)
                .WithMany(p => p.SkillOfLevels)
                .HasForeignKey(d => d.SkillId);
        }
}
}
