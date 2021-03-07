﻿namespace DataAccessLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using EducationPortal.Domain.Entities;

    public interface IRepository<T>
    {
        IList<T> GetAll();

        T Get(int id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);

        IList<T> GetAll(params Expression<Func<T, object>>[] includes);

        void Save();

        bool Exist(Expression<Func<T, bool>> predicat);

        IList<T> GetPage(
            Expression<Func<T, bool>> predicat,
            int skip,
            int take);

        IList<T> GetPageWithInclude(Expression<Func<T, object>> predicat, int skip, int take);

        IList<T> GetPage(int skip, int take);

        IList<TResult> Get<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicat);

        IList<TResult> Get<TResult>(Expression<Func<T, TResult>> selector);

        IList<T> Get(Expression<Func<T, bool>> predicat);

        T GetLastEntity<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy);

        T GetOne(Expression<Func<T, bool>> predicat);

        int Count();
    }
}
