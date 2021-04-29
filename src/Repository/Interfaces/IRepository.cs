using Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Interfaces
{
    public interface IRepository<T, in F> where T : class, IEntity<F>, new() where F : IEquatable<F>
    {
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(F id);
        Task<T> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(F id, T entity);
        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAsync(F id);
        Task<T> DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}