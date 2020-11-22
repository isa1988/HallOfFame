using System;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;

namespace HallOfFame.Service.Dto.AreaPerson.AreaSkill
{
    public class SkillOfPersonDto : IServiceDto
    {
        /// <summary>
        /// Навык
        /// </summary>
        public SkillOfLevelDto SkillOfLevel { get; set; }

        /// <summary>
        /// Навык
        /// </summary>
        public long SkillOfLevelId { get; set; }

        /// <summary>
        /// Человек
        /// </summary>
        public PersonDto Person { get; set; }

        /// <summary>
        /// Человек
        /// </summary>
        public long PersonId { get; set; }
    }
}
