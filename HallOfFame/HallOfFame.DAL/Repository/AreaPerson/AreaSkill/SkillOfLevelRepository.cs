using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallOfFame.Core.Contracts.AreaPerson.AreaSkill;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using HallOfFame.Core.Helper;
using HallOfFame.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.DAL.Repository.AreaPerson.AreaSkill
{
    public class SkillOfLevelRepository : Repository<SkillOfLevel, long>, ISkillOfLevelRepository
    {
        public SkillOfLevelRepository(HallOfFameContex context) 
            : base(context)
        {
        }

        public async Task<List<SkillOfLevel>> GetSkillByPerson(long personId, ResolveOptions resolveOptions = null)
        {
            var query = ResolveInclude(resolveOptions, false);
            
            if (resolveOptions == null || (!resolveOptions.IsPersons))
                query = query.Include(x => x.SkillsOfPersons);
            
            var entities = await query.Where(x => x.SkillsOfPersons.Any(n => n.PersonId == personId)).ToListAsync();
            ClearDbSetForInclude(entities);
            
            return entities;
        }

        public async Task<SkillOfLevel> GetSkillByLevel(long skillId, byte level, ResolveOptions resolveOptions = null)
        {
            var query = ResolveInclude(resolveOptions, false);

            var entity = await query.FirstOrDefaultAsync(x => x.SkillId == skillId && x.Level == level);
            ClearDbSetForInclude(entity);

            return entity;
        }
        protected override void ClearDbSetForInclude(SkillOfLevel entity)
        {
            if (entity.SkillsOfPersons != null)
            {
                for (int i = 0; i < entity.SkillsOfPersons.Count; i++)
                {
                    entity.SkillsOfPersons[i].SkillOfLevel = null;
                }
            }
        }
        
        protected override IQueryable<SkillOfLevel> ResolveInclude(ResolveOptions resolveOptions, bool isDelete)
        {
            IQueryable<SkillOfLevel> query = dbSet.Where(x => x.IsDelete == isDelete);
            
            if (resolveOptions == null)
            {
                return query;
            }

            if (resolveOptions.IsPersons)
            {
                query = query.Include(x => x.SkillsOfPersons);
                if (resolveOptions.IsPerson)
                {
                    query = query.Include(x => x.SkillsOfPersons).ThenInclude(n => n.Person);
                }
            }

            return query;
        }
    }
}
