using System.Collections.Generic;
using HallOfFame.Service.Dto;

namespace HallOfFame.Service
{
    public class EntityListOperationResult<T>
        where T : IServiceDto
    {
        private EntityListOperationResult(List<T> entities)
        {
            Entities = entities;
        }

        private EntityListOperationResult()
        {
        }

        public bool IsSuccess { get; private set; }

        public List<T> Entities { get; }

        private string[] errors = null;

        public static EntityListOperationResult<T> Success(List<T> entity)
        {
            return new EntityListOperationResult<T>(entity)
            {
                IsSuccess = true
            };
        }

        public static EntityListOperationResult<T> Failure(params string[] errorMessages)
        {
            var result = new EntityListOperationResult<T>
            {
                IsSuccess = false,
                errors = errorMessages
            };

            return result;
        }

        public EntityListOperationResult<T> AddError(params string[] errorMessages)
        {
            if (errorMessages?.Length > 0)
            {
                errors = errorMessages;
            }
            else
            {
                errors = null;
            }
            return this;
        }

        public string GetErrorString()
        {
            if (errors == null)
                return string.Empty;

            return string.Join(" ", errors);
        }
    }
}
