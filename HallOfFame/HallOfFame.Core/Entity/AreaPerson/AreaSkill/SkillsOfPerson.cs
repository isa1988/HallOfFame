using System;
using System.Collections.Generic;
using System.Text;

namespace HallOfFame.Core.Entity.AreaPerson.AreaSkill
{
    /// <summary>
    /// Навыки человека
    /// </summary>
    public class SkillsOfPerson : IEntity
    {
        /// <summary>
        /// Навык
        /// </summary>
        public SkillOfLevel SkillOfLevel { get; set; }

        /// <summary>
        /// Навык
        /// </summary>
        public Guid SkillOfLevelId { get; set; }

        /// <summary>
        /// Человек
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Человек
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
