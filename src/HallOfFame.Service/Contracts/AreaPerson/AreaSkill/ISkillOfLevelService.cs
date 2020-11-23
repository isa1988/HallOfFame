using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;

namespace HallOfFame.Service.Contracts.AreaPerson.AreaSkill
{
    public interface ISkillOfLevelService : IGeneralService<SkillOfLevelDto, SkillOfLevelEditDto, long>
    {
        Task<EntityListOperationResult<SkillDto>> UpdateListSkillAsync(List<string> names);
        Task<EntityListOperationResult<SkillOfLevelDto>> UpdateListSkillOfLevelAsync(List<SkillOfLevelEditDto> skillOfLevelEditDtos, List<SkillDto> skills);

        Task<EntityListOperationResult<SkillOfPersonDto>> UpdateListSkillOfPersonAsync(List<SkillOfLevelEditDto> skills,
            long personId, bool isCreate = true);
    }
}
