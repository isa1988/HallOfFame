using System;
using System.Collections.Generic;

namespace HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel
{
    public class SkillOfLevelEditDto : IServiceDto<long>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Начальный уровень
        /// </summary>
        public byte StartLevel { get; set; }

        /// <summary>
        /// Конечный уровень
        /// </summary>
        public byte EndLevel { get; set; }

        public bool IsDelete { get; set; }
        public long SkillId { get; set; }
    }
}
