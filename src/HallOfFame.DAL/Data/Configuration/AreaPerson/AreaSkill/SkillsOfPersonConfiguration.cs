using System;
using System.Collections.Generic;
using System.Text;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HallOfFame.DAL.Data.Configuration.AreaPerson.AreaSkill
{
    class SkillsOfPersonConfiguration :  IEntityTypeConfiguration<SkillOfPerson>
    {
        public void Configure(EntityTypeBuilder<SkillOfPerson> builder)
        {
            builder.HasKey(e => new { e.PersonId, e.SkillOfLevelId});

            builder.HasOne(d => d.Person)
                .WithMany(p => p.SkillsOfPersons)
                .HasForeignKey(d => d.PersonId);

            builder.HasOne(d => d.SkillOfLevel)
                .WithMany(p => p.SkillsOfPersons)
                .HasForeignKey(d => d.SkillOfLevelId);
        }
}
}
