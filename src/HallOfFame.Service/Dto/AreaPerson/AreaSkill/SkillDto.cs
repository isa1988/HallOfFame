using System;
using System.Collections.Generic;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;

namespace HallOfFame.Service.Dto.AreaPerson.AreaSkill
{
    public class SkillDto : IServiceDto<long>
    {
        public SkillDto()
        {
            SkillOfLevels = new List<SkillOfLevelDto>();
            Name = string.Empty;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

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
