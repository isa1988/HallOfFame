using System;
using System.Collections.Generic;
using System.Text;

namespace HallOfFame.Core.Entity.AreaPerson.AreaSkill
{
    public class Skill : IEntity<Guid>
    {
        public Skill()
        {
            SkillOfLevels = new List<SkillOfLevel>();
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
        /// Пометка на удаление
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// Уровни
        /// </summary>
        public List<SkillOfLevel> SkillOfLevels { get; set; }
    }
}
