using System;
using System.Threading.Tasks;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;

namespace HallOfFame.Core.Contracts.AreaPerson.AreaSkill
{
    public interface ISkillRepository : IRepository<Skill, long>
    {
        Task<bool> IsEqualsAsyncTask(string name);
    }
}
