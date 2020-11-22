using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HallOfFame.Service.Dto.AreaPerson;

namespace HallOfFame.Service.Contracts.AreaPerson
{
    public interface IPersonSkillService : IGeneralService<PersonDto, PersonEditDto, long>
    {
        Task<EntityOperationResult<PersonDto>> EditAsync(PersonEditDto editDto);
    }
}
