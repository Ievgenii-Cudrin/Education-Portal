namespace DataAccessLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using EducationPortal.Domain.Entities;

    public interface IRepository<T>
    {
        Task<IList<T>> GetAll();

        Task<T> Get(int id);

        Task Add(T item);

        Task Update(T item);

        Task Delete(int id);

        Task<IList<T>> GetAll(params Expression<Func<T, object>>[] includes);

        Task Save();

        Task<bool> Exist(Expression<Func<T, bool>> predicat);

        Task<IList<T>> GetPage(
            Expression<Func<T, bool>> predicat,
            int skip,
            int take);

        Task<IList<T>> GetPageWithInclude(Expression<Func<T, object>> predicat, int skip, int take);

        Task<IList<T>> GetPage(int skip, int take);

        Task<IList<TResult>> Get<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicat);

        Task<IList<TResult>> Get<TResult>(Expression<Func<T, TResult>> selector);

        Task<IList<T>> Get(Expression<Func<T, bool>> predicat);

        Task<T> GetLastEntity<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);

        Task<T> GetOne(Expression<Func<T, bool>> predicat);

        Task<int> Count();
    }
}
