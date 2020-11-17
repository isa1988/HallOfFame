using System;
using System.Collections.Generic;
using System.Text;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HallOfFame.DAL.Data.Configuration.AreaPerson.AreaSkill
{
    class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}
