using Store.Contracts.ViewModel;
using Store.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Services
{
    public interface IBaseService<TDomainEntity> : IDisposable
        where TDomainEntity : class, IBaseEntity
    {
        Task<TDomainEntity> GetById(long id, Func<IQueryable<TDomainEntity>, IQueryable<TDomainEntity>> includeAction = null);

        Task<int> Count(Expression<Func<TDomainEntity, bool>> predicate = null);

        Task<ICollection<TDomainEntity>> GetAll(Func<IQueryable<TDomainEntity>, IQueryable<TDomainEntity>> includeAction = null);

        Task<ICollection<TDomainEntity>> Find(Expression<Func<TDomainEntity, bool>> predicate = null, Func<IQueryable<TDomainEntity>,
            IQueryable<TDomainEntity>> includeAction = null,
            int? page = null, int? perPage = null, string orderBy = null, string orderDirection = null, string culture = null);

        Task<DataTableSearchViewModel<TDomainEntity>> Search(int? page = null, int? perPage = null, string sort = null,
            Expression<Func<TDomainEntity, bool>> predicate = null, Func<IQueryable<TDomainEntity>, IQueryable<TDomainEntity>> includes = null, string culture = null);

        Task<TDomainEntity> Add(TDomainEntity entity);

        Task<TDomainEntity>Update(TDomainEntity entity);

        Task UpdateBatch(IEnumerable<TDomainEntity> entity);

        Task Remove(long id);

        Task<TValue> Max<TValue>(Expression<Func<TDomainEntity, TValue>> property, Expression<Func<TDomainEntity, bool>> predicate = null);

        Task<TValue> Min<TValue>(Expression<Func<TDomainEntity, TValue>> property, Expression<Func<TDomainEntity, bool>> predicate = null);

        void ReleaseEntity(TDomainEntity entity);

    }
}