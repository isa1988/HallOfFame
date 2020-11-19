using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HallOfFame.Core.Contracts.AreaPerson.AreaSkill;
using HallOfFame.Core.Entity.AreaPerson.AreaSkill;
using HallOfFame.Core.Helper;
using HallOfFame.Service.Contracts.AreaPerson.AreaSkill;
using HallOfFame.Service.Dto.AreaPerson.AreaSkill.SkillOfLevel;

namespace HallOfFame.Service.Services.AreaPerson.AreaSkill
{
    public class SkillOfLevelService : GeneralService<SkillOfLevel, SkillOfLevelDto, SkillOfLevelEditDto, long>, ISkillOfLevelService
    {
        public SkillOfLevelService(IMapper mapper, ISkillOfLevelRepository repository, ISkillRepository skillRepository) : base(mapper, repository)
        {
            this.skillRepository = skillRepository;
        }

        private readonly ISkillRepository skillRepository;

        public override ResolveOptions GetOptionsForDeteils()
        {
            return new ResolveOptions
            {
                IsPersons = true,
                IsPerson = true
            };
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
