using System;
using System.Collections.Generic;
using System.Text;
using HallOfFame.Core.Entity.AreaPerson;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using HallOfFame.DAL.Data.Configuration.AreaPerson;
using HallOfFame.DAL.Data.Configuration.AreaPerson.AreaSkill;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.DAL.Data
{
    public class HallOfFameContex : DbContext
    {
        public HallOfFameContex(DbContextOptions<HallOfFameContex> options)
            : base(options)
        {
        }

        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SkillOfLevel> SkillOfLevels { get; set; }
        public virtual DbSet<SkillOfPerson> SkillOfPeoples { get; set; }

        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
            modelBuilder.ApplyConfiguration(new SkillOfLevelConfiguration());
            modelBuilder.ApplyConfiguration(new SkillsOfPersonConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
