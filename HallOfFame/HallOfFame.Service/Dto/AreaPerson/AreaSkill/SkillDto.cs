using System;
using System.Collections.Generic;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;

namespace HallOfFame.Service.Dto.AreaPerson.AreaSkill
{
    public class SkillDto : IServiceDto<Guid>
    {
        public SkillDto()
        {
            SkillOfLevels = new List<SkillOfLevelDto>();
            Name = string.Empty;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уровни
        /// </summary>
        public List<SkillOfLevelDto> SkillOfLevels { get; set; }
    }
}
