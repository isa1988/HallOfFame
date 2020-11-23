using System;
using System.Collections.Generic;
using AutoMapper;
using HallOfFame.Service.Dto.AreaPerson;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;
using HallOfFame.WebAPI.Models;
using HallOfFame.WebAPI.Models.Person;

namespace HallOfFame.WebAPI.AppStart.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            SkillMapping();
            PersonMapping();
        }

        private void PersonMapping()
        {
            CreateMap<PersonModel, PersonDto>()
                .ForMember(p => p.FirstName, n => n.MapFrom(m => m.Name))
                .ForMember(p => p.SurName, n => n.MapFrom(m => m.DisplayName))
                .ForMember(p => p.Id, n => n.Ignore())
                .ForMember(p => p.SkillsOfPersons, n => n.Ignore());


            CreateMap<PersonDto, PersonModel>()
                .ForMember(p => p.Name, n => n.MapFrom(m => m.FirstName))
                .ForMember(p => p.DisplayName, n => n.MapFrom(m => m.SurName))
                .ForMember(p => p.Skills, n => n.MapFrom(m => m.SkillsOfPersons));
            
            CreateMap<PersonDto, PersonOneModel>()
                .ForMember(p => p.Name, n => n.MapFrom(m => m.FirstName))
                .ForMember(p => p.DisplayName, n => n.MapFrom(m => m.SurName))
                .ForMember(p => p.Skills, n => n.MapFrom(m => m.SkillsOfPersons));

            CreateMap<PersonModel, PersonEditDto>()
                .ForMember(p => p.FirstName, n => n.MapFrom(m => m.Name))
                .ForMember(p => p.SurName, n => n.MapFrom(m => m.DisplayName));
        }

        private void SkillMapping()
        {
            CreateMap<SkillModel, SkillOfLevelDto>()
                .ForMember(p => p.Id, n => n.Ignore())
                .ForMember(p => p.IsDelete, n => n.Ignore())
                .ForMember(p => p.Skill, n => n.Ignore())
                .ForMember(p => p.SkillsOfPersons, n => n.Ignore())
                .ForMember(p => p.SkillId, n => n.Ignore());
            CreateMap<SkillOfLevelDto, SkillModel>();
            CreateMap<SkillModel, SkillOfLevelEditDto>()
                .ForMember(p => p.StartLevel, n => n.MapFrom(m => m.Level))
                .ForMember(p => p.Id, n => n.Ignore())
                .ForMember(p => p.IsDelete, n => n.Ignore())
                .ForMember(p => p.EndLevel, n => n.Ignore())
                .ForMember(p => p.SkillId, n => n.Ignore());

            CreateMap<SkillOfPersonDto, SkillModel>()
                .ForMember(p => p.Name, n =>
                    n.MapFrom(x => x.SkillOfLevel != null && x.SkillOfLevel.Skill != null ? x.SkillOfLevel.Skill.Name : ""))
                .ForMember(p => p.Level, n =>
                    n.MapFrom(x => x.SkillOfLevel != null ? x.SkillOfLevel.Level : 0));
        }
    }
}
