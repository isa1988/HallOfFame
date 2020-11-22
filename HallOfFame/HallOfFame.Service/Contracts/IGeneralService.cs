using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HallOfFame.Core.Entity;
using HallOfFame.Service.Dto;

namespace HallOfFame.Service.Contracts
{
    public interface IGeneralService<TDto, TDtoCreate>
        where TDto : IServiceDto
        where TDtoCreate : IServiceDto
    {
        /// <summary>
        /// Добавить запись в базу
        /// </summary>
        /// <param name="createDto">Объект добавление</param>
        /// <returns></returns>
        Task<EntityOperationResult<TDto>> CreateAsync(TDtoCreate createDto);

        /// <summary>
        /// Вернуть все записи
        /// </summary>
        /// <returns></returns>
        Task<List<TDto>> GetAllAsync();

        /// <summary>
        /// Вернуть все записи со всеми зависимостями
        /// </summary>
        /// <returns></returns>
        Task<List<TDto>> GetAllDeteilsAsync();

        /// <summary>
        /// Вернуть по странично
        /// </summary>
        /// <param name="numPage">Номер страницы</param>
        /// <param name="pageSize">Записей на странице</param>
        /// <returns></returns>
        Task<List<TDto>> GetPageAsync(int numPage, int pageSize);

        /// <summary>
        /// Вернуть по странично со всеми зависимостями
        /// </summary>
        /// <param name="numPage">Номер страницы</param>
        /// <param name="pageSize">Записей на странице</param>
        /// <returns></returns>
        Task<List<TDto>> GetPageDeteilsAsync(int numPage, int pageSize);
    }

    public interface IGeneralService<TDto, TDtoCreate, TId> : IGeneralService<TDto, TDtoCreate>
        where TDto : IServiceDto<TId>
        where TDtoCreate : IServiceDto<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Вернуть конкретный объект из базы
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns></returns>
        Task<EntityOperationResult<TDto>> GetByIdAsync(TId id);

        /// <summary>
        /// Вернуть конкретный объект из базы со всеми зависимостями
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns></returns>
        Task<EntityOperationResult<TDto>> GetByIdDeteilsAsync(TId id);

        /// <summary>
        /// Пометить на удаление
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns></returns>
        Task<EntityOperationResult<TDto>> DeleteItemAsync(TId id);
        /// <summary>
        /// Удалить объект из БД
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns></returns>
        Task<EntityOperationResult<TDto>> DeleteItemFromDbAsync(TId id);
    }
}
