using System.Threading.Tasks;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using HallOfFame.Core.Helper;

namespace HallOfFame.Core.Contracts.AreaPerson.AreaSkill
{
    public interface ISkillOfPersonRepository : IRepository<SkillOfPerson>
    {
        Task<SkillOfPerson> GetByPersonAndSkill(long personId, long skillId, ResolveOptions resolveOptions = null);
    }
}
