using HallOfFame.WebAPI.Models.Skill;
using System.Collections.Generic;

namespace HallOfFame.WebAPI.Models.Person
{
    public class PersonEditModel
    {
        public PersonEditModel()
        {
            Skills = new List<SkillEditModel>();
        }

        public long Id { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<SkillEditModel> Skills { get; set; }
    }
}
