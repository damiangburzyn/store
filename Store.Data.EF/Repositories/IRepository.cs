using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Data.EF.Entities;

namespace Store.Data
{ 
    public interface IRepository : IDisposable
    {
        Task<TEntity> GetById<TEntity>(long id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeAction = null)
            where TEntity : class, IBaseEntity;

        Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IBaseEntity;

        Task<ICollection<TEntity>> GetAll<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeAction = null)
            where TEntity : class, IBaseEntity;

        Task<ICollection<TEntity>> Find<TEntity>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeAction = null,
            int? page = null, int? perPage = null, string orderBy = null, string orderDirection = null, string culture = null)
            where TEntity : class, IBaseEntity;

        Task<TValue> Max<TEntity, TValue>(Expression<Func<TEntity, TValue>> property, Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IBaseEntity;

        Task<TValue> Min<TEntity, TValue>(Expression<Func<TEntity, TValue>> property, Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IBaseEntity;

        Task Add<TEntity>(TEntity obj)
            where TEntity : class, IBaseEntity;

        Task AddBatch<TEntity>(IEnumerable<TEntity> objs)
            where TEntity : class, IBaseEntity;

        void Update<TEntity>(TEntity obj)
            where TEntity : class, IBaseEntity;

        void UpdateBatch<TEntity>(IEnumerable<TEntity> obj)
            where TEntity : class, IBaseEntity;

        void Remove<TEntity>(long id)
            where TEntity : class, IBaseEntity;

        void RemoveBatch<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IBaseEntity;

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        void Detach<TEntity>(TEntity obj) where TEntity : class, IBaseEntity;

        void Attach<TEntity>(TEntity obj, EntityState state = EntityState.Unchanged) where TEntity : class, IBaseEntity;
    }
}