using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using HallOfFame.Service.Contracts.AreaPerson;
using HallOfFame.Service.Dto.AreaPerson;
using HallOfFame.WebAPI.Models.Person;
using Microsoft.AspNetCore.Mvc;

namespace HallOfFame.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonController : ControllerBase
    {
        public PersonController(IMapper mapper, IPersonSkillService personSkillService)
        {
            this.mapper = mapper;
            this.personSkillService = personSkillService;
        }

        private readonly IMapper mapper;
        private readonly IPersonSkillService personSkillService;

        /// <summary>
        /// Вытащить всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PersonModel>> GetAll()
        {
            var dtos = await personSkillService.GetAllDeteilsAsync();
            var models = mapper.Map<List<PersonModel>>(dtos);
            return models;
        }

        /// <summary>
        /// Показать сотрудника по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        public async Task<PersonOneModel> GetById(long id)
        {
            var dto = await personSkillService.GetByIdDeteilsAsync(id);
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

        /// <summary>
        /// Добавить нового сотрудника
        /// </summary>
        /// <param name="model">сотрудник</param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<HttpResponseMessage> Add([FromBody] PersonCreateModel model)
        {

            HttpResponseMessage returnMessage = new HttpResponseMessage();

            var personEditDto = mapper.Map<PersonEditDto>(model);
            var result = await personSkillService.CreateAsync(personEditDto);
            if (result.IsSuccess)
            {

                string message = ($"Person Created - {result.Entity.Id}");
                returnMessage = new HttpResponseMessage(HttpStatusCode.Created);
                returnMessage.RequestMessage = new HttpRequestMessage(HttpMethod.Post, message);
            }
            else
            {
                returnMessage = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                returnMessage.RequestMessage = new HttpRequestMessage(HttpMethod.Post, result.GetErrorString());
            }


            return returnMessage;
        }


        /// <summary>
        /// Редактировать сотрудника
        /// </summary>
        /// <param name="model">сотрудник</param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<HttpResponseMessage> Edit([FromBody] PersonEditModel model)
        {

            HttpResponseMessage returnMessage = new HttpResponseMessage();

            var personEditDto = mapper.Map<PersonEditDto>(model);
            var result = await personSkillService.EditAsync(personEditDto);
            if (result.IsSuccess)
            {

                string message = ($"Person Update - {result.Entity.Id}");
                returnMessage = new HttpResponseMessage(HttpStatusCode.OK);
                returnMessage.RequestMessage = new HttpRequestMessage(HttpMethod.Put, message);
            }
            else
            {
                returnMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                returnMessage.RequestMessage = new HttpRequestMessage(HttpMethod.Put, result.GetErrorString());
            }


            return returnMessage;
        }


        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="model">сотрудник</param>
        /// <returns></returns>
        [HttpDelete("Delete{id:long}")]
        public async Task<HttpResponseMessage> Delete(long id)
        {

            HttpResponseMessage returnMessage = new HttpResponseMessage();

            var result = await personSkillService.DeleteItemFromDbAsync(id);
            if (result.IsSuccess)
            {

                string message = ($"Person Delete - {result.Entity.Id}");
                returnMessage = new HttpResponseMessage(HttpStatusCode.OK);
                returnMessage.RequestMessage = new HttpRequestMessage(HttpMethod.Put, message);
            }
            else
            {
                returnMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                returnMessage.RequestMessage = new HttpRequestMessage(HttpMethod.Put, result.GetErrorString());
            }


            return returnMessage;
        }


    }
}
