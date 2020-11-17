using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallOfFame.Core.Contracts.AreaPerson;
using HallOfFame.Core.Entity.AreaPerson;
using HallOfFame.Core.Helper;
using HallOfFame.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.DAL.Repository.AreaPerson
{
    public class PersonRepository : RepositoryOfGIdGuid<Person>, IPersonRepository
    {
        public PersonRepository(HallOfFameContex context)
            : base(context)
        {
        }
        public async Task<List<Person>> GetPersonsBySkill(Guid skillOfLevelId, ResolveOptions resolveOptions)
        {
            var query = ResolveInclude(resolveOptions);

            if (resolveOptions == null || (!resolveOptions.IsSkill))
                query = query.Include(x => x.SkillsOfPersons);

            var entities = await query.Where(x => x.SkillsOfPersons.Any(n => n.SkillOfLevelId == skillOfLevelId)).ToListAsync();
            ClearDbSetForInclude(entities);

            return entities;
        }
        
        protected override void ClearDbSetForInclude(Person entity)
        {
            if (entity.SkillsOfPersons != null)
            {
                for (int i = 0; i < entity.SkillsOfPersons.Count; i++)
                {
                    entity.SkillsOfPersons[i].Person = null;
                }
            }
        }

        protected override IQueryable<Person> ResolveInclude(ResolveOptions resolveOptions)
        {
            IQueryable<Person> query = dbSet;

            if (resolveOptions == null)
            {
                return query;
            }

            if (resolveOptions.IsSkills)
            {
                query = query.Include(x => x.SkillsOfPersons);
                if (resolveOptions.IsSkill)
                {
                    query = query.Include(x => x.SkillsOfPersons).ThenInclude(n => n.SkillOfLevel);
                }
            }

            return query;
        }
    }
}
