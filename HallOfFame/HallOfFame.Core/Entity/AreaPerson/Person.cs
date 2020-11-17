using System;
using System.Collections.Generic;
using System.Text;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;

namespace HallOfFame.Core.Entity.AreaPerson
{
    /// <summary>
    /// Человек
    /// </summary>
    public class Person : IEntity<Guid>
    {
        public Person()
        {
            SkillsOfPersons = new List<SkillsOfPerson>();
            SurName = string.Empty;
            FirstName = string.Empty;
        }
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public List<SkillsOfPerson> SkillsOfPersons { get; set; }
    }
}
