using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HallOfFame.Core.Contracts.AreaPerson;
using HallOfFame.Core.Entity.AreaPerson;
using HallOfFame.Core.Helper;
using HallOfFame.Service.Contracts.AreaPerson;
using HallOfFame.Service.Contracts.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;

namespace HallOfFame.Service.Services.AreaPerson
{
    public class PersonSkillService : GeneralService<Person, PersonDto, PersonEditDto, long>, IPersonSkillService
    {
        public PersonSkillService(IMapper mapper, IPersonRepository repository, ISkillOfLevelService skillOfLevelService)
            : base(mapper, repository)
        {
            this.skillOfLevelService = skillOfLevelService;
            personRepository = repository;
        }

        private readonly ISkillOfLevelService skillOfLevelService;
        private readonly IPersonRepository personRepository;

        public override async Task<EntityOperationResult<PersonDto>> CreateAsync(PersonEditDto createDto)
        {
            string errors = CheckBeforeModification(createDto);
            if (!string.IsNullOrEmpty(errors))
            {
                return EntityOperationResult<PersonDto>.Failure().AddError(errors);
            }

            try
            {
                var lstSkill = createDto.Skills.Select(x => x.Name).Distinct().ToList();
                var skills = new List<SkillDto>();
                var resultSkill = await skillOfLevelService.UpdateListSkillAsync(lstSkill);
                if (resultSkill.IsSuccess)
                {
                    skills.AddRange(resultSkill.Entities);
                }
                else
                {
                    return EntityOperationResult<PersonDto>.Failure().AddError(resultSkill.GetErrorString());
                }

                var skillOfLevels = new List<SkillOfLevelEditDto>();
                var resultSkillOfLevel = await skillOfLevelService.UpdateListSkillOfLevelAsync(createDto.Skills, skills);

                if (resultSkillOfLevel.IsSuccess)
                {
                    skillOfLevels = mapper.Map<List<SkillOfLevelEditDto>>(resultSkillOfLevel.Entities);
                }
                else
                {
                    return EntityOperationResult<PersonDto>.Failure().AddError(resultSkillOfLevel.GetErrorString());
                }

                var person = new Person
                {
                    FirstName = createDto.FirstName,
                    SurName = createDto.SurName,
                };
                var entity = await personRepository.AddAsync(person);
                await personRepository.SaveAsync();

                for (int i = 0; i < skillOfLevels.Count; i++)
                {
                    skillOfLevels[i].IsDelete = createDto.Skills.FirstOrDefault(x =>
                        x.Name == skillOfLevels[i].Name &&
                        x.StartLevel == skillOfLevels[i].StartLevel)?.IsDelete ?? false;
                }
                var resultSkillsOfPerson = await skillOfLevelService.UpdateListSkillOfPersonAsync(skillOfLevels, entity.Id);

                var dto = mapper.Map<PersonDto>(entity);

                return EntityOperationResult<PersonDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<PersonDto>.Failure().AddError(ex.Message);
            }
        }


        public async Task<EntityOperationResult<PersonDto>> EditAsync(PersonEditDto editDto)
        {
            string errors = CheckBeforeModification(editDto);
            if (!string.IsNullOrEmpty(errors))
            {
                return EntityOperationResult<PersonDto>.Failure().AddError(errors);
            }

            try
            {
                var person = await personRepository.GetByIdAsync(editDto.Id);
                if (person == null)
                {
                    return EntityOperationResult<PersonDto>.Failure().AddError("Человека с таким ключом не существует");
                }

                person.FirstName = editDto.FirstName;
                person.SurName = editDto.SurName;
                personRepository.Update(person);

                var lstSkill = editDto.Skills.Select(x => x.Name).Distinct().ToList();
                var skills = new List<SkillDto>();
                var resultSkill = await skillOfLevelService.UpdateListSkillAsync(lstSkill);
                if (resultSkill.IsSuccess)
                {
                    skills.AddRange(resultSkill.Entities);
                }
                else
                {
                    return EntityOperationResult<PersonDto>.Failure().AddError(resultSkill.GetErrorString());
                }

                var skillOfLevels = new List<SkillOfLevelEditDto>();
                var resultSkillOfLevel = await skillOfLevelService.UpdateListSkillOfLevelAsync(editDto.Skills,  skills);

                if (resultSkillOfLevel.IsSuccess)
                {
                    skillOfLevels = mapper.Map<List<SkillOfLevelEditDto>>(resultSkillOfLevel.Entities);
                }
                else
                {
                    return EntityOperationResult<PersonDto>.Failure().AddError(resultSkillOfLevel.GetErrorString());
                }

                for (int i = 0; i < skillOfLevels.Count; i++)
                {
                    skillOfLevels[i].IsDelete = editDto.Skills.FirstOrDefault(x =>
                        x.Name == skillOfLevels[i].Name &&
                        x.StartLevel == skillOfLevels[i].StartLevel)?.IsDelete ?? false;
                    skillOfLevels[i].SkillId = skills.FirstOrDefault(x => x.Name.ToLower() == skillOfLevels[i].Name.ToLower()).Id;
                }
                var resultSkillsOfPerson = await skillOfLevelService.UpdateListSkillOfPersonAsync(skillOfLevels, editDto.Id, false);


                await personRepository.SaveAsync();


                var dto = mapper.Map<PersonDto>(person);

                return EntityOperationResult<PersonDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<PersonDto>.Failure().AddError(ex.Message);
            }
        }

        public override ResolveOptions GetOptionsForDeteils()
        {
            return new ResolveOptions
            {
                IsSkills = true,
                IsSkill = true
            };
        }

        protected override string CheckBeforeModification(PersonEditDto value, bool isNew = true)
        {
            StringBuilder errors = new StringBuilder(string.Empty);
            if (value == null)
                errors.Append("Не передан объект для действий");

            if (string.IsNullOrWhiteSpace(value.FirstName))
                errors.Append("Не заполнено имя человека");
            if (string.IsNullOrWhiteSpace(value.SurName))
                errors.Append("Не заполнено фамилия человека");

            if (value.Skills?.Count > 0)
            {
                foreach (var skill in value.Skills)
                {
                    if (string.IsNullOrWhiteSpace(skill.Name))
                        errors.Append("Не заполненно наименование навыка");
                    if (skill.StartLevel <= 0)
                        errors.Append($"Начальный уровень навыка {skill.Name} не может быть меньше 1");
                    if (skill.EndLevel > 10)
                        errors.Append($"Конечный уровень навыка {skill.Name} не может быть больше 10");
                    if (skill.EndLevel > 10)
                        errors.Append($"Начальный уровень навыка {skill.Name} не может быть больше 10");
                }
            }

            
            
            return errors.ToString();
        }

        protected override string CkeckBeforeDelete(Person entity)
        {
            throw new NotImplementedException();
        }

    }
}
