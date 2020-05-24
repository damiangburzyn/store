
using Store.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Store.Data.EF.Enums;
using Store.Data.Database;

namespace Store.Data.EF.Repositories.Base
{
    public class Repository :IRepository
    {
        protected ApplicationDbContext DbContext;

        public Repository(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public virtual async Task<TEntity> GetById<TEntity>(long id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeAction = null)
            where TEntity : class, IBaseEntity
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            if (includeAction != null)
            {
                query = includeAction(query);
            }

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IBaseEntity
        {
            if (predicate == null)
            {
                return await DbContext.Set<TEntity>().CountAsync();
            }

            return await DbContext.Set<TEntity>().Where(predicate).CountAsync();
        }

        public virtual async Task<ICollection<TEntity>> GetAll<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeAction = null)
            where TEntity : class, IBaseEntity
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            if (includeAction != null)
            {
                query = includeAction(query);
            }

            return await query.ToArrayAsync();
        }

        public virtual async Task<ICollection<TEntity>> Find<TEntity>(
            Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>,
            IQueryable<TEntity>> includeAction = null,
            int? page = null, int? perPage = null, string orderBy = null, string orderDirection = null, string culture = null)
            where TEntity : class, IBaseEntity
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeAction != null)
            {
                query = includeAction(query);
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                var prop = TypeDescriptor.GetProperties(typeof(TEntity)).Find(orderBy, true);

                if (!string.IsNullOrEmpty(orderDirection) &&
                    string.Equals(orderDirection, ESortDirection.Desc.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    query = query.OrderByDescending(x => prop.GetValue(x));
                }
                else
                {
                    query = query.OrderBy(x => prop.GetValue(x));
                }
            }

            if (page.HasValue && perPage.HasValue)
            {
                query = query.Skip((page.Value - 1) * perPage.Value).Take(perPage.Value);
            }

            return await query.ToArrayAsync();
        }

        public virtual async Task Add<TEntity>(TEntity obj)
            where TEntity : class, IBaseEntity
        {
            await DbContext.Set<TEntity>().AddAsync(obj);
        }

        public virtual async Task AddBatch<TEntity>(IEnumerable<TEntity> objs)
            where TEntity : class, IBaseEntity
        {
            await DbContext.Set<TEntity>().AddRangeAsync(objs);
        }

        public virtual void Update<TEntity>(TEntity obj)
            where TEntity : class, IBaseEntity
        {
            if (!(obj.Id > 0))
            {
                return;
            }

            if (!DbContext.ChangeTracker.Entries<TEntity>().Any(x => x.IsKeySet && x.Entity.Id == obj.Id))
            {
                DbContext.Set<TEntity>().Attach(obj);
            }

            var entityEntry = DbContext.ChangeTracker.Entries<TEntity>().SingleOrDefault(x => x.IsKeySet && x.Entity.Id == obj.Id);

            if (entityEntry != null)
            {
                entityEntry.State = EntityState.Modified;
            }
        }

        public virtual async Task<TValue> Max<TEntity, TValue>(Expression<Func<TEntity, TValue>> property, Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IBaseEntity
        {
            if (predicate == null)
            {
                return await DbContext.Set<TEntity>().MaxAsync(property);
            }

            return await DbContext.Set<TEntity>().Where(predicate).MaxAsync(property);
        }

        public virtual async Task<TValue> Min<TEntity, TValue>(Expression<Func<TEntity, TValue>> property, Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class, IBaseEntity
        {
            if (predicate == null)
            {
                return await DbContext.Set<TEntity>().MinAsync(property);
            }

            return await DbContext.Set<TEntity>().Where(predicate).MinAsync(property);
        }


        public virtual void UpdateBatch<TEntity>(IEnumerable<TEntity> list)
           where TEntity : class, IBaseEntity
        {
            foreach (var obj in list)
            {
                if (!(obj.Id > 0))
                {
                    return;
                }

                if (!DbContext.ChangeTracker.Entries<TEntity>().Any(x => x.IsKeySet && x.Entity.Id == obj.Id))
                {
                    DbContext.Set<TEntity>().Attach(obj);
                }

                var entityEntry = DbContext.ChangeTracker.Entries<TEntity>().SingleOrDefault(x => x.IsKeySet && x.Entity.Id == obj.Id);

                if (entityEntry != null)
                {
                    entityEntry.State = EntityState.Modified;
                }
            }
        }

        public virtual void Remove<TEntity>(long id)
            where TEntity : class, IBaseEntity
        {
            var dbSet = DbContext.Set<TEntity>();

            dbSet.Remove(dbSet.Find(id));
        }

        public virtual void RemoveBatch<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IBaseEntity
        {
            var dbSet = DbContext.Set<TEntity>();

            dbSet.RemoveRange(entities);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual void Detach<TEntity>(TEntity obj)
            where TEntity : class, IBaseEntity
        {
            DbContext.Entry(obj).State = EntityState.Detached;
        }

        public virtual void Attach<TEntity>(TEntity obj, EntityState state = EntityState.Unchanged)
            where TEntity : class, IBaseEntity
        {
            if (obj.Id <= 0)
            {
                return;
            }

            if (!DbContext.ChangeTracker.Entries<TEntity>().Any(x => x.IsKeySet && x.Entity.Id == obj.Id))
            {
                DbContext.Set<TEntity>().Attach(obj);
            }

            var entityEntry = DbContext.ChangeTracker.Entries<TEntity>().SingleOrDefault(x => x.IsKeySet && x.Entity.Id == obj.Id);

            if (entityEntry == null)
            {
                return;
            }

            entityEntry.State = state;
        }

        public void Dispose()
        {
            DbContext.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}