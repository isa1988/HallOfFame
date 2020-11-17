using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HallOfFame.Core.Contracts.AreaPerson.AreaSkill;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using HallOfFame.Core.Helper;
using HallOfFame.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.DAL.Repository.AreaPerson.AreaSkill
{
    public class SkillRepository : RepositoryOfGIdGuid<Skill>, ISkillRepository
    {
        public SkillRepository(HallOfFameContex context)
            : base(context)
        {
        }
        
        protected override void ClearDbSetForInclude(Skill entity)
        {
            if (entity.SkillOfLevels?.Count > 0)
            {
                for (int i = 0; i < entity.SkillOfLevels.Count; i++)
                {
                    entity.SkillOfLevels[i].Skill = null;
                }
            }
        }

        protected override IQueryable<Skill> ResolveInclude(ResolveOptions resolveOptions)
        {
            IQueryable<Skill> query = dbSet;

            if (resolveOptions.IsSkillOfLevels)
            {
                query = query.Include(x => x.SkillOfLevels);
            }

            return query;
        }
    }
}
