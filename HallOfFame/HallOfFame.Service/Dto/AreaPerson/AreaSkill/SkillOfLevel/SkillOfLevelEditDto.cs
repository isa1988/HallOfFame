﻿using System;
using System.Collections.Generic;

namespace HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel
{
    public class SkillOfLevelEditDto : IServiceDto<Guid>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
    }
}
