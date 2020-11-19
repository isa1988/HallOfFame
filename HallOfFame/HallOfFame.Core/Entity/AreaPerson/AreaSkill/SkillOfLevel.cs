using System;
using System.Collections.Generic;
using System.Text;

namespace HallOfFame.Core.Entity.AreaPerson.AreaSkill
{
    /// <summary>
    /// Навык
    /// </summary>
    public class SkillOfLevel : IEntity<Guid>
    {
        public SkillOfLevel()
        {
            SkillsOfPersons = new List<SkillOfPerson>();
        }
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навык
        /// </summary>
        public Skill Skill { get; set; }

        /// <summary>
        /// Навык
        /// </summary>
        public Guid SkillId { get; set; }

        /// <summary>
        /// Уровень
        /// </summary>
        public byte Level { get; set; }

        /// <summary>
        /// Пометка на удаление
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// Люди
        /// </summary>
        public List<SkillOfPerson> SkillsOfPersons { get; set; }
    }
}
