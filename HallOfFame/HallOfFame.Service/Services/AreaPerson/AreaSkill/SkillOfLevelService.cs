using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HallOfFame.Core.Contracts.AreaPerson.AreaSkill;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using HallOfFame.Core.Helper;
using HallOfFame.Service.Contracts.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;

namespace HallOfFame.Service.Services.AreaPerson.AreaSkill
{
    public class SkillOfLevelService : GeneralService<SkillOfLevel, SkillOfLevelDto, SkillOfLevelEditDto, long>, ISkillOfLevelService
    {
        public SkillOfLevelService(IMapper mapper, 
                                   ISkillOfLevelRepository repository, 
                                   ISkillRepository skillRepository,
                                   ISkillOfPersonRepository skillOfPersonRepository) 
            : base(mapper, repository)
        {
            this.skillRepository = skillRepository;
            this.skillOfLevelRepository = repository;
            this.skillOfPersonRepository = skillOfPersonRepository;
        }

        private readonly ISkillRepository skillRepository;
        private readonly ISkillOfLevelRepository skillOfLevelRepository;
        private readonly ISkillOfPersonRepository skillOfPersonRepository;
        public override ResolveOptions GetOptionsForDeteils()
        {
            return new ResolveOptions
            {
                IsPersons = true,
                IsPerson = true
            };
        }

        public async Task<EntityListOperationResult<SkillDto>> UpdateListSkillAsync(List<string> names)
        {
            try
            {
                var skills = new List<Skill>();
                bool isCreateSkkill = false;
                Skill skillEntity = null;
                for (int i = 0; i < names.Count; i++)
                {
                    try
                    {
                        skillEntity = await skillRepository.GetByNameAsync(names[i]);
                        skills.Add(skillEntity);
                    }
                    catch (Exception e)
                    {

                        var skillNew = new Skill
                        {
                            Name = names[i],
                        };
                        skills.Add(skillNew);
                        isCreateSkkill = true;
                    }
                }

                if (isCreateSkkill)
                    await skillRepository.SaveAsync();
                var skillDtos = mapper.Map<List<SkillDto>>(skills);
                return EntityListOperationResult<SkillDto>.Success(skillDtos);
            }
            catch (Exception ex)
            {
                return EntityListOperationResult<SkillDto>.Failure().AddError(ex.Message);
            }
        }

        public async Task<EntityListOperationResult<SkillOfLevelDto>> UpdateListSkillOfLevelAsync(List<SkillOfLevelEditDto> skills, bool isCreate = true)
        {
            try
            {
                var skillOfLevels = new List<SkillOfLevel>();
                SkillOfLevel skillOfLevel = null;
                long idSkill = 0;
                bool isCreateSkkill = false;
                foreach (var skill in skills)
                {
                    idSkill = skills.FirstOrDefault(x => x.Name == skill.Name).Id;
                    if (isCreate)
                    {
                        skillOfLevel = new SkillOfLevel
                        {
                            SkillId = idSkill,
                            Level = skill.StartLevel
                        };
                        isCreateSkkill = true;
                        skillOfLevel = await skillOfLevelRepository.AddAsync(skillOfLevel);
                        skillOfLevels.Add(skillOfLevel);
                    }
                    else
                    {
                        try
                        {
                            skillOfLevel = await skillOfLevelRepository.GetSkillByLevel(idSkill, skill.StartLevel);
                            skillOfLevels.Add(skillOfLevel);
                        }
                        catch (Exception e)
                        {
                            skillOfLevel = new SkillOfLevel
                            {
                                SkillId = idSkill,
                                Level = skill.StartLevel
                            };
                            isCreateSkkill = true;
                            skillOfLevel =  await skillOfLevelRepository.AddAsync(skillOfLevel);
                            skillOfLevels.Add(skillOfLevel);
                        }
                    }
                }
                if (isCreateSkkill)
                    await skillOfLevelRepository.SaveAsync();
                var skillDtos = mapper.Map<List<SkillOfLevelDto>>(skillOfLevels);
                return EntityListOperationResult<SkillOfLevelDto>.Success(skillDtos);
            }
            catch (Exception ex)
            {
                return EntityListOperationResult<SkillOfLevelDto>.Failure().AddError(ex.Message);
            }
        }

        public async Task<EntityListOperationResult<SkillOfPersonDto>> UpdateListSkillOfPersonAsync(List<SkillOfLevelEditDto> skills, long personId, bool isCreate = true)
        {
            try
            {
                var skillOfPersons = new List<SkillOfPerson>();
                SkillOfPerson skillOfPerson = null;
                bool isCreateSkkill = false;
                foreach (var skill in skills)
                {
                    if (isCreate)
                    {
                        skillOfPerson = new SkillOfPerson
                        {
                            SkillOfLevelId = skill.Id,
                            PersonId = skill.StartLevel
                        };
                        isCreateSkkill = true;
                        skillOfPerson = await skillOfPersonRepository.AddAsync(skillOfPerson);
                        skillOfPersons.Add(skillOfPerson);
                    }
                    else
                    {
                        try
                        {
                            skillOfPerson = await skillOfPersonRepository.GetByPersonAndSkill(personId, skill.StartLevel);
                            if (skill.IsDelete)
                            {
                                skillOfPersonRepository.DeleteFromDB(skillOfPerson);
                            }
                            else
                            {
                                skillOfPersons.Add(skillOfPerson);
                            }
                        }
                        catch (Exception e)
                        {
                            skillOfPerson = new SkillOfPerson
                            {
                                SkillOfLevelId = skill.Id,
                                PersonId = skill.StartLevel
                            };
                            isCreateSkkill = true;
                            skillOfPerson = await skillOfPersonRepository.AddAsync(skillOfPerson);
                            skillOfPersons.Add(skillOfPerson);
                        }
                    }
                }
                if (isCreateSkkill)
                    await skillOfPersonRepository.SaveAsync();
                var skillDtos = mapper.Map<List<SkillOfPersonDto>>(skillOfPersons);
                return EntityListOperationResult<SkillOfPersonDto>.Success(skillDtos);
            }
            catch (Exception ex)
            {
                return EntityListOperationResult<SkillOfPersonDto>.Failure().AddError(ex.Message);
            }
        }

        public override async Task<EntityOperationResult<SkillOfLevelDto>> CreateAsync(SkillOfLevelEditDto createDto)
        {
            string errors = CheckBeforeModification(createDto);
            if (!string.IsNullOrEmpty(errors))
            {
                return EntityOperationResult<SkillOfLevelDto>.Failure().AddError(errors);
            }

            try
            {
                var skill = new Skill
                {
                    Name = createDto.Name,
                };
                skill = await skillRepository.AddAsync(skill);
                await skillRepository.SaveAsync();
                SkillOfLevel entity = null;
                for (byte i = createDto.StartLevel; i <= createDto.EndLevel; i++)
                {
                    entity = await repositoryBase.AddAsync(new SkillOfLevel
                    {
                        Level = i,
                        SkillId = skill.Id,
                    });
                }
                await repositoryBase.SaveAsync();

                var dto = mapper.Map<SkillOfLevelDto>(entity);

                return EntityOperationResult<SkillOfLevelDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<SkillOfLevelDto>.Failure().AddError(ex.Message);
            }
        }

        /*public async Task<EntityOperationResult<SkillOfLevelDto>> EditAsync(SkillOfLevelEditDto editDto)
        {
            string errors = CheckBeforeModification(editDto, false);
            if (!string.IsNullOrEmpty(errors))
            {
                return EntityOperationResult<SkillOfLevelDto>.Failure().AddError(errors);
            }

            try
            {
                var skill = await skillRepository.GetByNameAsync(editDto.Name, new ResolveOptions {IsSkillOfLevels = true});
                skill.Name = editDto.Name;
                skill = await skillRepository.AddAsync(skill);
                
                for (byte i = 0; i <= skill.SkillOfLevels.Count; i++)
                {
                    if (skill.SkillOfLevels[i].Level >= editDto.StartLevel &&
                        skill.SkillOfLevels[i].Level <= editDto.EndLevel)
                    {
                        repositoryBase.UnDelete(skill[]);
                    }
                    else
                    {
                        
                    }
                    
                }
                await repositoryBase.SaveAsync();

                var dto = mapper.Map<SkillOfLevelDto>(entity);

                return EntityOperationResult<SkillOfLevelDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<SkillOfLevelDto>.Failure().AddError(ex.Message);
            }
        }*/

        protected override string CheckBeforeModification(SkillOfLevelEditDto value, bool isNew = true)
        {
            StringBuilder errors = new StringBuilder(string.Empty);
            if (value == null)
                errors.Append("Не передан объект для действий");
            if (string.IsNullOrWhiteSpace(value.Name))
                errors.Append("Не заполненно имя");
            if (value.StartLevel <= 0)
                errors.Append("Начальный уровень не может быть меньше 1");
            if (value.EndLevel > 10)
                errors.Append("Конечный уровень не может быть больше 10");
            if (value.EndLevel > 10)
                errors.Append("Начальный уровень не может быть больше 10");
            if (isNew)
            {
                if (skillRepository.IsEqualsAsyncTask(value.Name).Result)
                {
                    errors.Append("Такой навык уже есть");
                }
            }
            return errors.ToString();
        }

        protected override string CkeckBeforeDelete(SkillOfLevel entity)
        {
            return string.Empty;
        }
    }
}
