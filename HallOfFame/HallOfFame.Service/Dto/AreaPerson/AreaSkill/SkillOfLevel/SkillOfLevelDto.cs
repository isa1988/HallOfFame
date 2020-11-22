using System;
using System.Collections.Generic;

namespace HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel
{
    public class SkillOfLevelDto : IServiceDto<long>
    {
        public SkillOfLevelDto()
        {
            SkillsOfPersons = new List<SkillOfPersonDto>();
        }
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        public byte StartLevel { get; set; }

        /// <summary>
        /// Навык
        /// </summary>
        public SkillDto Skill { get; set; }

        /// <summary>
        /// Навык
        /// </summary>
        public long SkillId { get; set; }

        /// <summary>
        /// Уровень
        /// </summary>
        public byte Level { get; set; }

        public bool IsDelete { get; set; }

        /// <summary>
        /// Люди
        /// </summary>
        public List<SkillOfPersonDto> SkillsOfPersons { get; set; }
    }
}
