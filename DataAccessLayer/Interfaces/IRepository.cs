using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        Task Add(T item);

        Task Update(T item);

        Task Delete(int id);

        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);

        Task Save();

        Task<bool> Exist(Expression<Func<T, bool>> predicat);

        Task<IEnumerable<T>> GetPage(
            Expression<Func<T, bool>> predicat,
            int skip,
            int take);

        Task<IEnumerable<T>> GetPageWithInclude(Expression<Func<T, object>> predicat, int skip, int take);

        Task<IEnumerable<T>> GetPage(int skip, int take);

        Task<IEnumerable<TResult>> Get<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicat);

        Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<T, TResult>> selector);

        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicat);

        Task<T> GetLastEntity<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);

        Task<T> GetOne(Expression<Func<T, bool>> predicat);

        Task<int> Count();

        Task Delete(T entity);

        Task<int> GetCountWithPredicate(Expression<Func<T, bool>> predicat);

        Task<IEnumerable<T>> GetWithInclude(Expression<Func<T, bool>> predicat, Expression<Func<T, object>> param);
    }
}
