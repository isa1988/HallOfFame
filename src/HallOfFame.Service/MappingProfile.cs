using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using HallOfFame.Core.Entity.AreaPerson;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;

namespace HallOfFame.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            SkillMapping();
            SkillOfLevelMapping();
            SkillofPersonMapping();
            PersonMapping();
        }

        private void SkillMapping()
        {
            CreateMap<SkillDto, Skill>()
                .ForMember(x => x.SkillOfLevels, p => p.Ignore());
            CreateMap<Skill, SkillDto>();
        }

        private void SkillOfLevelMapping()
        {
            CreateMap<SkillOfLevelEditDto, SkillOfLevel>()
                .ForMember(x => x.SkillsOfPersons, p => p.Ignore())
                .ForMember(x => x.Level, p => p.MapFrom(m => m.StartLevel));
            CreateMap<SkillOfLevelDto, SkillOfLevel>()
                .ForMember(x => x.SkillsOfPersons, p => p.Ignore());
            CreateMap<SkillOfLevel, SkillOfLevelDto>();
            CreateMap<SkillOfLevelDto, SkillOfLevelEditDto>()
                .ForMember(x => x.EndLevel, p => p.Ignore())
                .ForMember(x => x.StartLevel, p => p.MapFrom(m => m.Level));
        }

        private void SkillofPersonMapping()
        {
            CreateMap<SkillOfPersonDto, SkillOfPerson>()
                .ForMember(x => x.SkillOfLevel, p => p.Ignore())
                .ForMember(x => x.Person, p => p.Ignore());
            CreateMap<SkillOfPerson, SkillOfPersonDto>();
        }

        private void PersonMapping()
        {
            CreateMap<PersonDto, Person>()
                .ForMember(x => x.SkillsOfPersons, p => p.Ignore());
            CreateMap<Person, PersonDto>();
        }
    }
}
