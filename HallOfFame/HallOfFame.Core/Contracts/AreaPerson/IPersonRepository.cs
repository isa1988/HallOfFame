using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HallOfFame.Core.Entity.AreaPerson;

namespace HallOfFame.Core.Contracts.AreaPerson
{
    public interface IPersonRepository : IRepository<Person, Guid>
    {
        Task<List<Person>> GetPersonsBySkill(Guid skillOfLevelId);
    }
}
