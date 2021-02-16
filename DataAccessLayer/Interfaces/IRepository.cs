namespace DataAccessLayer.Interfaces
{
    using EducationPortal.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<T>
        where T : class
    {
        public IList<T> GetAll();

        public T Get(int id);

        public void Add(T item);

        public void Update(T item);

        public void Delete(int id);

        public IList<T> GetAll(params Expression<Func<T, object>>[] includes);

        public void Save();

        public bool Exist(Expression<Func<T, bool>> predicat);

        public IList<T> GetPage(
            Expression<Func<T, bool>> predicat,
            PageInfo page);

        public IList<T> GetPage(PageInfo page);

        public IList<TResult> Get<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicat);

        public IList<TResult> Get<TResult>(Expression<Func<T, TResult>> selector);

        public IList<T> Get(
            Expression<Func<T, bool>> predicat,
            params Expression<Func<T, object>>[] includes);
    }
}
