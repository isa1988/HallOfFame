﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfFame.WebAPI.Models.Skill
{
    public class SkillCreateModel
    {
        public string Name { get; set; }

        public byte Level { get; set; }
    }
}
