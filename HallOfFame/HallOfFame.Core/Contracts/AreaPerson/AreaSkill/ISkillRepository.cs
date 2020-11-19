using System;
using System.Threading.Tasks;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;

namespace HallOfFame.Core.Contracts.AreaPerson.AreaSkill
{
    public interface ISkillRepository : IRepository<Skill, Guid>
    {
        Task<bool> IsEqualsAsyncTask(string name);
    }
}
