namespace EducationPortal.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataAccessLayer.Interfaces;
    using EducationPortal.DAL.DataContext;
    using EducationPortal.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class RepositorySql<T> : IRepository<T>
        where T : class
    {
        private readonly ApplicationContext dbContext;

        public RepositorySql(ApplicationContext context)
        {
            this.dbContext = context;
        }

        public IList<T> GetAll()
        {
            return this.dbContext.Set<T>().ToList();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var result = this.dbContext.Set<T>();
            foreach (var include in includes)
            {
                result.Include(include);
            }

            return result.ToList();
        }

        public IList<T> Get(
            Expression<Func<T, bool>> predicat,
            params Expression<Func<T, object>>[] includes)
        {
            var result = this.dbContext.Set<T>();
            foreach (var include in includes)
            {
                result.Include(include);
            }

            return result.Where(predicat).ToList();
        }

        public IList<TResult> Get<TResult>(Expression<Func<T, TResult>> selector)
        {
            return this.dbContext.Set<T>()
                        .Select(selector).ToList();
        }

        public IList<TResult> Get<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicat)
        {
            return this.dbContext.Set<T>()
                        .Where(predicat)
                        .Select(selector).ToList();
        }

        public IList<T> GetPage(PageInfo page)
        {
            var skip = page.Size * (page.Number - 1);
            return this.dbContext.Set<T>()
                        .Skip(skip).Take(page.Size).ToList();
        }

        public IList<T> GetPage(
            Expression<Func<T, bool>> predicat,
            PageInfo page)
        {
            var skip = page.Size * (page.Number - 1);
            return this.dbContext.Set<T>()
                        .Where(predicat)
                        .Skip(skip).Take(page.Size).ToList();
        }

        // public IList<TEntity> GetPage<TEntity>(
        //    Expression<Func<TEntity, TResult>> selector,
        //    Expression<Func<TEntity, bool>> predicat,
        //    PageInfo page)
        //    where TEntity : class
        // {
        //    var skip = page.Size * (page.Number - 1);
        //    return this.dbContext.Set<TEntity>()
        //                .Select(selector)
        //                .Where(predicat)
        //                .Skip(skip).Take(page.Size).ToList();
        // }
        public bool Exist(Expression<Func<T, bool>> predicat)
        {
            return this.dbContext.Set<T>().Any(predicat);
        }

        public void Add(T entity)
        {
            this.dbContext.Set<T>().Add(entity);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return this.dbContext.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            this.dbContext.Entry<T>(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = this.dbContext.Set<T>().Find(id);

            if (entity != null)
            {

                this.dbContext.Set<T>().Remove(entity);
            }
        }
    }
}
