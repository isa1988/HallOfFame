using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using HallOfFame.Core.Helper;

namespace HallOfFame.Core.Contracts.AreaPerson.AreaSkill
{
    public interface ISkillOfLevelRepository : IRepository<SkillOfLevel, Guid>
    {
        Task<List<SkillOfLevel>> GetSkillByPerson(Guid personId, ResolveOptions resolveOptions);
    }
}
