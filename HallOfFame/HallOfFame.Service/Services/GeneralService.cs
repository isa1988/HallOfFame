using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HallOfFame.Core.Contracts;
using HallOfFame.Core.Entity;
using HallOfFame.Core.Helper;
using HallOfFame.Service.Contracts;
using HallOfFame.Service.Dto;

namespace HallOfFame.Service.Services
{
    public abstract class GeneralService<TEntity, TDto, TDtoCreate> : IGeneralService<TDto, TDtoCreate>
        where TEntity : class, IEntity
        where TDto : class, IServiceDto
        where TDtoCreate: class, IServiceDto
    {
        public GeneralService(IMapper mapper, IRepository<TEntity> repository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            repositoryBase = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected readonly IMapper mapper;
        protected readonly IRepository<TEntity> repositoryBase;
        protected abstract string CheckBeforeModification(TDtoCreate value, bool isNew = true);

        public virtual async Task<EntityOperationResult<TDto>> CreateAsync(TDtoCreate createDto)
        {
            string errors = CheckBeforeModification(createDto);
            if (!string.IsNullOrEmpty(errors))
            {
                return EntityOperationResult<TDto>.Failure().AddError(errors);
            }

            try
            {
                TEntity value = mapper.Map<TEntity>(createDto);
                var entity = await repositoryBase.AddAsync(value);
                await repositoryBase.SaveAsync();

                var dto = mapper.Map<TDto>(entity);

                return EntityOperationResult<TDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<TDto>.Failure().AddError(ex.Message);
            }
        }

        public abstract ResolveOptions GetOptionsForDeteils();

        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var dtoList = await repositoryBase.GetAllAsync();
            return mapper.Map<List<TDto>>(dtoList);
        }

        public virtual async Task<List<TDto>> GetAllDeteilsAsync()
        {
            var dtoList = await repositoryBase.GetAllAsync(GetOptionsForDeteils());
            return mapper.Map<List<TDto>>(dtoList);
        }

        public virtual async Task<List<TDto>> GetPageAsync(int numPage, int pageSize)
        {
            var dtoList = await repositoryBase.GetAllOfPageAsync(numPage, pageSize);
            return mapper.Map<List<TDto>>(dtoList);
        }

        public virtual async Task<List<TDto>> GetPageDeteilsAsync(int numPage, int pageSize)
        {
            var dtoList = await repositoryBase.GetAllOfPageAsync(numPage, pageSize, GetOptionsForDeteils());
            return mapper.Map<List<TDto>>(dtoList);
        }
    }

    public abstract class GeneralService<TEntity, TDto, TDtoCreate, TId> : GeneralService<TEntity, TDto, TDtoCreate>, IGeneralService<TDto, TDtoCreate, TId>
        where TEntity : class, IEntity<TId>
        where TDto : class, IServiceDto<TId>
        where TDtoCreate : class, IServiceDto<TId>
        where TId : IEquatable<TId>
    {
        public GeneralService(IMapper mapper, IRepository<TEntity, TId> repository) : base(mapper, repository)
        {
            repositoryBaseId = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected readonly IRepository<TEntity, TId> repositoryBaseId;

        protected abstract string CkeckBeforeDelete(TEntity entity);

        public virtual async Task<EntityOperationResult<TDto>> GetByIdAsync(TId id)
        {
            try
            {
                TEntity entity = await repositoryBaseId.GetByIdAsync(id);
                var dto = mapper.Map<TDto>(entity);
                return EntityOperationResult<TDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<TDto>.Failure().AddError(ex.Message);
            }
        }

        public virtual async Task<EntityOperationResult<TDto>> GetByIdDeteilsAsync(TId id)
        {
            try
            {
                TEntity entity = await repositoryBaseId.GetByIdAsync(id, GetOptionsForDeteils());
                var dto = mapper.Map<TDto>(entity);
                return EntityOperationResult<TDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<TDto>.Failure().AddError(ex.Message);
            }
        }

        public virtual async Task<EntityOperationResult<TDto>> DeleteItemAsync(TId id)
        {
            try
            {
                TEntity entity = await repositoryBaseId.GetByIdAsync(id);

                string error = CkeckBeforeDelete(entity);
                if (!string.IsNullOrEmpty(error))
                {
                    return EntityOperationResult<TDto>.Failure().AddError(error);
                }

                var dto = mapper.Map<TDto>(entity);
                repositoryBaseId.Delete(entity);
                await repositoryBaseId.SaveAsync();
                return EntityOperationResult<TDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<TDto>.Failure().AddError(ex.Message);
            }
        }
    }
}
