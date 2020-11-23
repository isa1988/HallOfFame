using HallOfFame.WebAPI.Models.Skill;
using System.Collections.Generic;

namespace HallOfFame.WebAPI.Models.Person
{
    public class PersonCreateModel
    {
        public PersonCreateModel()
        {
            Skills = new List<SkillCreateModel>();
        }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<SkillCreateModel> Skills { get; set; }
    }
}
