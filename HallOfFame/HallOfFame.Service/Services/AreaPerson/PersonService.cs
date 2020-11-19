using System;
using AutoMapper;
using HallOfFame.Core.Contracts.AreaPerson;
using HallOfFame.Core.Entity.AreaPerson;
using HallOfFame.Core.Helper;
using HallOfFame.Service.Contracts.AreaPerson;
using HallOfFame.Service.Dto.AreaPerson;

namespace HallOfFame.Service.Services.AreaPerson
{
    public class PersonService : GeneralService<Person, PersonDto, PersonDto, long>, IPersonService
    {
        public PersonService(IMapper mapper, IPersonRepository repository) : base(mapper, repository)
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
