﻿using HallOfFame.WebAPI.Models.Skill;
using System.Collections.Generic;

namespace HallOfFame.WebAPI.Models.Person
{
    public class PersonOneModel : ErrorModel
    {
        public PersonOneModel()
        {
            Skills = new List<SkillModel>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<SkillModel> Skills { get; set; }
    }
}
