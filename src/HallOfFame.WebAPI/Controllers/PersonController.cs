using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HallOfFame.Service.Contracts.AreaPerson;
using HallOfFame.WebAPI.Models.Person;
using Microsoft.AspNetCore.Mvc;

namespace HallOfFame.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public PersonController(IMapper mapper, IPersonService personService)
        {
            this.mapper = mapper;
            this.personService = personService;
        }

        private readonly IMapper mapper;
        private readonly IPersonService personService;

        /// <summary>
        /// Вытащить всех людей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PersonModel>> GetAll()
        {
            var dtos = await personService.GetAllDeteilsAsync();
            var models = mapper.Map<List<PersonModel>>(dtos);
            return models;
        }

        /// <summary>
        /// Показать человека по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        public async Task<PersonOneModel> GetById(long id)
        {
            var dto = await personService.GetByIdDeteilsAsync(id);
            if (dto.IsSuccess)
            {
                var model = mapper.Map<PersonOneModel>(dto.Entity);
                return model;
            }
            else
            {
                return new PersonOneModel
                {
                    IsError = true, 
                    Error = dto.GetErrorString()
                };
            }
        }
    }
}
