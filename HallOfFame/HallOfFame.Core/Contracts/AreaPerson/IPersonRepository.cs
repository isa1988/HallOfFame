using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HallOfFame.Core.Entity.AreaPerson;
using HallOfFame.Core.Helper;

namespace HallOfFame.Core.Contracts.AreaPerson
{
    public interface IPersonRepository : IRepository<Person, long>
    {
        Task<List<Person>> GetPersonsBySkill(long skillOfLevelId, ResolveOptions resolveOptions);
    }
}
