using System;
using AutoMapper;
using HallOfFame.Core.Contracts;
using HallOfFame.Core.Entity.AreaPerson;
using HallOfFame.Core.Helper;
using HallOfFame.Service.Contracts.AreaPerson;
using HallOfFame.Service.Dto.AreaPerson;

namespace HallOfFame.Service.Services.AreaPerson
{
    public class PersonService : GeneralService<Person, PersonDto, PersonDto, Guid>, IPersonService
    {
        public PersonService(IMapper mapper, IRepository<Person, Guid> repository) : base(mapper, repository)
        {
        }

        public override ResolveOptions GetOptionsForDeteils()
        {
            return new ResolveOptions
            {
                IsSkills = true,
                IsSkill = true
            };
        }

        protected override string CheckBeforeModification(PersonDto value, bool isNew = true)
        {
            return string.Empty;
        }

        protected override string CkeckBeforeDelete(Person entity)
        {
            return string.Empty;
        }
    }
}
