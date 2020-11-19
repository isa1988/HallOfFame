using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallOfFame.Core.Contracts;
using HallOfFame.Core.Entity;
using HallOfFame.Core.Helper;
using HallOfFame.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HallOfFame.DAL.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        public Repository(HallOfFameContex contextDB)
        {
            this.contextDB = contextDB;
            this.dbSet = this.contextDB.Set<T>();
        }

        protected HallOfFameContex contextDB;
        protected DbSet<T> dbSet { get; set; }

        public virtual async Task<T> AddAsync(T entity)
        {
            var entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }
        public virtual T Add(T entity)
        {
            var entry = dbSet.Add(entity);

            return entry.Entity;
        }

        public virtual async Task<List<T>> GetAllOfPageAsync(int pageNumber, int rowCount, ResolveOptions resolveOptions = null)
        {
            int startIndex = (pageNumber - 1) * rowCount;
            var entities =  await ResolveInclude(resolveOptions, false)
                   .Skip(startIndex)
                   .Take(rowCount)
                   .ToListAsync();
            ClearDbSetForInclude(entities);
            return entities;
        }

        //identity
        public virtual async Task<List<T>> GetAllAsync(ResolveOptions resolveOptions = null)
        {
            var entities = await ResolveInclude(resolveOptions, false).ToListAsync();
            ClearDbSetForInclude(entities);
            return entities;
        }

        public virtual async Task<List<T>> GetDeleteAllOfPageAsync(int pageNumber, int rowCount, ResolveOptions resolveOptions = null)
        {
            int startIndex = (pageNumber - 1) * rowCount;
            var entities = await ResolveInclude(resolveOptions, true)
                .Skip(startIndex)
                .Take(rowCount)
                .ToListAsync();
            ClearDbSetForInclude(entities);
            return entities;
        }

        //identity
        public virtual async Task<List<T>> GetDeleteAllAsync(ResolveOptions resolveOptions = null)
        {
            var entities = await ResolveInclude(resolveOptions, true).ToListAsync();
            ClearDbSetForInclude(entities);
            return entities;
        }

        protected virtual void ClearDbSetForInclude(List<T> entities)
        {
            foreach (var entity in entities)
            {
                ClearDbSetForInclude(entity);
            }
        }
        protected abstract void ClearDbSetForInclude(T entity);

        public virtual EntityEntry<T> Update(T entity)
        {
            var entry = dbSet.Update(entity);
            return entry;
        }

        public virtual EntityEntry<T> DeleteFromDB(T entity)
        {
            var entry = dbSet.Remove(entity);
            return entry;
        }


        public virtual void Delete(T entity)
        {
            entity.IsDelete = true;
        }


        public virtual void UnDelete(T entity)
        {
            entity.IsDelete = false;
        }

        public virtual async Task SaveAsync()
        {
            await contextDB.SaveChangesAsync();
        }

        public virtual void Save()
        {
            contextDB.SaveChanges();
        }
        protected abstract IQueryable<T> ResolveInclude(ResolveOptions resolveOptions, bool isDelete);
    }

    public abstract class Repository<T, TId> : Repository<T>, IRepository<T, TId>
        where T : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        public Repository(HallOfFameContex contextDB)
            : base(contextDB)
        {
        }

        public virtual async Task<T> GetByIdAsync(TId id, ResolveOptions resolveOptions = null)
        {
            var entity = await ResolveInclude(resolveOptions, false).FirstOrDefaultAsync(x => !x.Id.Equals(id));
            if (entity == null)
                throw new NullReferenceException("Не найдено значение по идентификатору");
            ClearDbSetForInclude(entity);
            return entity;
        }


        public virtual async Task<T> GetDeleteByIdAsync(TId id, ResolveOptions resolveOptions = null)
        {
            var entity = await ResolveInclude(resolveOptions, true).FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity == null)
                throw new NullReferenceException("Не найдено значение по идентификатору");
            ClearDbSetForInclude(entity);
            return entity;
        }
    }

    public abstract class RepositoryOfGIdGuid<T> : Repository<T, Guid>, IRepository<T, Guid>
        where T : class, IEntity<Guid>
    {
        public RepositoryOfGIdGuid(HallOfFameContex contextDB)
            : base(contextDB)
        {
        }

        public override async Task<T> AddAsync(T entity)
        {
            entity.Id = GetId(Guid.NewGuid());

            return await base.AddAsync(entity);
        }

        private Guid GetId(Guid value)
        {
            if (dbSet.Any(p => p.Id.Equals(value)))
            {

                value = Guid.NewGuid();
                return GetId(value);
            }

            return value;
        }
    }
}
