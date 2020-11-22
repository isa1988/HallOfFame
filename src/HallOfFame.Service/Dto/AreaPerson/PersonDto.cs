using System;
using System.Collections.Generic;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill;

namespace HallOfFame.Service.Dto.AreaPerson
{
    public class PersonDto : IServiceDto<long>
    {
        public PersonDto()
        {
            SkillsOfPersons = new List<SkillOfPersonDto>();
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
        public List<SkillOfPersonDto> SkillsOfPersons { get; set; }
    }
}
