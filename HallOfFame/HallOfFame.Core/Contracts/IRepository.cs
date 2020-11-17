using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HallOfFame.Core.Entity;
using HallOfFame.Core.Helper;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HallOfFame.Core.Contracts
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> AddAsync(T entity);
        T Add(T entity);
        Task<List<T>> GetAllOfPageAsync(int pageNumber, int rowCount, ResolveOptions resolveOptions = null);
        Task<List<T>> GetAllAsync(ResolveOptions resolveOptions = null);
        EntityEntry<T> Update(T entity);
        EntityEntry<T> Delete(T entity);
        Task SaveAsync();
        void Save();
    }
    public interface IRepository<T, TId> : IRepository<T>
        where T : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        Task<T> GetByIdAsync(TId id, ResolveOptions resolveOptions = null);
    }
}
