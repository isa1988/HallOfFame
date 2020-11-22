using System;
using System.Collections.Generic;
using System.Text;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;

namespace HallOfFame.Service.Dto.AreaPerson
{
    public class PersonEditDto : IServiceDto<long>
    {
        public PersonEditDto()
        {
            Skills = new List<SkillOfLevelEditDto>();
            SurName = string.Empty;
            FirstName = string.Empty;
        }
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SurName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Навыки
        /// </summary>
        public List<SkillOfLevelEditDto> Skills { get; set; }
    }
}
