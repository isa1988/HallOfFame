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
    public class SkillRepository : Repository<Skill, long>, ISkillRepository
    {
        public SkillRepository(HallOfFameContex context)
            : base(context)
        {
        }

        public async Task<bool> IsEqualsAsyncTask(string name)
        {
            var isEquals = await dbSet.AnyAsync(x => x.Name.ToLower() == name.ToLower());
            return isEquals;
        }

        public virtual async Task<Skill> GetByNameAsync(string name, ResolveOptions resolveOptions = null)
        {
            var entity = await ResolveInclude(resolveOptions, false).FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            if (entity == null)
                throw new NullReferenceException("Не найдено значение по имени");
            ClearDbSetForInclude(entity);
            return entity;
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

        protected override IQueryable<Skill> ResolveInclude(ResolveOptions resolveOptions, bool isDelete)
        {
            IQueryable<Skill> query = dbSet.Where(x => x.IsDelete);

            if (resolveOptions.IsSkillOfLevels)
            {
                query = query.Include(x => x.SkillOfLevels);
            }

            return query;
        }
    }
}
