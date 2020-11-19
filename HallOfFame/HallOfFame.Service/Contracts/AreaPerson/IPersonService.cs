using System;
using HallOfFame.Service.Dto.AreaPerson;

namespace HallOfFame.Service.Contracts.AreaPerson
{
    public interface IPersonService : IGeneralService<PersonDto, PersonDto, Guid>
    {
    }
}
