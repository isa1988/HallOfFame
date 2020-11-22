using System;
using System.Threading.Tasks;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using HallOfFame.Core.Helper;

namespace HallOfFame.Core.Contracts.AreaPerson.AreaSkill
{
    public interface ISkillRepository : IRepository<Skill, long>
    {
        Task<bool> IsEqualsAsyncTask(string name);
        Task<Skill> GetByNameAsync(string name, ResolveOptions resolveOptions = null);
    }
}
