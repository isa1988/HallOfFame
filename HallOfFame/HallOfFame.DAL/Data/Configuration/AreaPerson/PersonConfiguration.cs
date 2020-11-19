using System;
using System.Collections.Generic;
using System.Text;
using HallOfFame.Core.Entity.AreaPerson;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HallOfFame.DAL.Data.Configuration.AreaPerson
{
    class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.SurName).IsRequired().HasMaxLength(100);

        }
    }
}
