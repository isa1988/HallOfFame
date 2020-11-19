using System;
using System.Collections.Generic;

namespace HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel
{
    public class SkillOfLevelDto : IServiceDto<Guid>
    {
        public SkillOfLevelDto()
        {
            SkillsOfPersons = new List<SkillOfPersonDto>();
        }
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public Guid SkillId { get; set; }

        /// <summary>
        /// Уровень
        /// </summary>
        public byte Level { get; set; }

        /// <summary>
        /// Люди
        /// </summary>
        public List<SkillOfPersonDto> SkillsOfPersons { get; set; }
    }
}
