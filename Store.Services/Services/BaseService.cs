
using AutoMapper;
using Store.Contracts.ViewModel;
using Store.Data;
using Store.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Services
{
    public abstract class BaseService<TDomainEntity> : IBaseService<TDomainEntity>
       where TDomainEntity : class, IBaseEntity
    {
        protected readonly IRepository Repository;

        protected BaseService(IRepository repository)
        {
            Repository = repository; 
        }

        public virtual async Task<TDomainEntity> GetById(long id, Func<IQueryable<TDomainEntity>, IQueryable<TDomainEntity>> includeAction = null)
        {
            return await Repository.GetById<TDomainEntity>(id, includeAction);
        }

        public async Task<int> Count(Expression<Func<TDomainEntity, bool>> predicate = null)
        {
            return await Repository.Count(predicate);
        }

        public virtual async Task<ICollection<TDomainEntity>> GetAll(Func<IQueryable<TDomainEntity>, IQueryable<TDomainEntity>> includeAction = null)
        {
            return await Repository.GetAll<TDomainEntity>(includeAction);
        }

        public virtual async Task<ICollection<TDomainEntity>> Find(
            Expression<Func<TDomainEntity, bool>> predicate = null, Func<IQueryable<TDomainEntity>,
            IQueryable<TDomainEntity>> includeAction = null,
            int? page = null, int? perPage = null, string orderBy = null, string orderDirection = null, string culture = null)
        {
            return await Repository.Find(predicate, includeAction, page, perPage, orderBy, orderDirection, culture);
        }

        public async Task<DataTableSearchViewModel<TDomainEntity>> Search(int? page = null, int? perPage = null, string sort = null,
            Expression<Func<TDomainEntity, bool>> predicate = null, Func<IQueryable<TDomainEntity>, IQueryable<TDomainEntity>> includes = null, string culture = null)
        {
            var currentPage = page.HasValue && page > 0 ? page.Value : 1;
            var validPerPage = perPage.HasValue && perPage > 0 && perPage <= 50 ? perPage.Value : 100;

            var result = await Find(predicate, includes, currentPage, validPerPage,
                !string.IsNullOrEmpty(sort) ? sort.Split('|')[0] : null, !string.IsNullOrEmpty(sort) ? sort.Split('|')[1] : null, culture);

            var total = await Count(predicate);
            var lastPage = (int)Math.Ceiling((decimal)total / validPerPage);
            var from = (currentPage - 1) * validPerPage + 1;


            var dataView = new DataTableSearchViewModel<TDomainEntity>
            {
                Data = result,
                Total = total,
                PerPage = validPerPage,
                CurrentPage = currentPage,
                LastPage = lastPage,
                From = from,
                To = from - 1 + result.Count
            };

            return dataView;
        }

        public virtual async Task<TValue> Max<TValue>(Expression<Func<TDomainEntity, TValue>> property, Expression<Func<TDomainEntity, bool>> predicate = null)
        {
            return await Repository.Max(property, predicate);
        }

        public virtual async Task<TValue> Min<TValue>(Expression<Func<TDomainEntity, TValue>> property, Expression<Func<TDomainEntity, bool>> predicate = null)
        {
            return await Repository.Min(property, predicate);
        }

        public virtual async Task<TDomainEntity> Add(TDomainEntity entity)
        {
            await Repository.Add(entity);

            await Repository.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TDomainEntity> Update(TDomainEntity entity)
        {
            Repository.Update(entity);

            await Repository.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateBatch(IEnumerable<TDomainEntity> entities)
        {
            Repository.UpdateBatch(entities);

            await Repository.SaveChangesAsync();
        }

        public virtual async Task Remove(long id)
        {
            var entity = await GetById(id);

            if (entity == null)
            {
                return;
            }

            Repository.Remove<TDomainEntity>(entity.Id);

            await Repository.SaveChangesAsync();
        }

        public virtual void ReleaseEntity(TDomainEntity entity)
        {
            Repository.Detach(entity);
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}