using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;

namespace HallOfFame.Core.Contracts.AreaPerson.AreaSkill
{
    public interface ISkillOfLevelRepository : IRepository<SkillOfLevel, Guid>
    {
        Task<List<SkillOfLevel>> GetSkillByPerson(Guid personId);
    }
}
