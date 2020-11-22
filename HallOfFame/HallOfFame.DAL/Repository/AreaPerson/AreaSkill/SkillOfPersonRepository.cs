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
    public class SkillOfPersonRepository : Repository<SkillOfPerson>, ISkillOfPersonRepository
    {
        public SkillOfPersonRepository(HallOfFameContex context)
            : base(context)
        {
        }

        public async Task<SkillOfPerson> GetByPersonAndSkill(long personId, long skillId, ResolveOptions resolveOptions = null)
        {
            var entity = await ResolveInclude(resolveOptions, false)
                                         .FirstOrDefaultAsync(x => x.PersonId == personId && x.SkillOfLevelId == skillId);
            if (entity == null)
                throw new NullReferenceException("Не найдено значение");
            ClearDbSetForInclude(entity);
            return entity;
        }

        protected override void ClearDbSetForInclude(SkillOfPerson entity)
        {
            
        }

        protected override IQueryable<SkillOfPerson> ResolveInclude(ResolveOptions resolveOptions, bool isDelete)
        {
            IQueryable<SkillOfPerson> query = dbSet.Where(x=> x.IsDelete == isDelete);

            if (resolveOptions.IsPerson)
            {
                query = query.Include(x => x.Person);
            }

            if (resolveOptions.IsSkillOfLevel)
            {
                query = query.Include(x => x.SkillOfLevel);
            }

            return query;
        }
    }
}
