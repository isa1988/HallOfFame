using HallOfFame.Service.Dto;

namespace HallOfFame.Service
{
    public class EntityOperationResult<T>
        where T : IServiceDto
    {
        private EntityOperationResult(T entity)
        {
            Entity = entity;
        }

        private EntityOperationResult()
        {
        }

        public bool IsSuccess { get; private set; }

        public T Entity { get; }

        private string[] errors = null;

        public static EntityOperationResult<T> Success(T entity)
        {
            return new EntityOperationResult<T>(entity)
            {
                IsSuccess = true
            };
        }

        public static EntityOperationResult<T> Failure(params string[] errorMessages)
        {
            var result = new EntityOperationResult<T>
            {
                IsSuccess = false,
                errors = errorMessages
            };

            return result;
        }

        public EntityOperationResult<T> AddError(params string[] errorMessages)
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
