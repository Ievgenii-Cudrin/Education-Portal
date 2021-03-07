namespace EducationPortal.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataAccessLayer.Interfaces;
    using EducationPortal.DAL.DataContext;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class RepositorySql<T> : IRepository<T>
        where T : class
    {
        private readonly ApplicationContext dbContext;
        private static ILogger logger;

        public RepositorySql(ILogger log, ApplicationContext context)
        {
            logger = log;
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

        public T GetOne(Expression<Func<T, bool>> predicat)
        {
            return this.dbContext.Set<T>()
                        .Where(predicat).Take(1).FirstOrDefault();
        }

        public IList<T> Get(Expression<Func<T, bool>> predicat)
        {
            return this.dbContext.Set<T>()
                        .Where(predicat).ToList();
        }

        public IList<T> GetPage(int skip, int take)
        {
            return this.dbContext.Set<T>()
                        .Skip(skip).Take(take).ToList();
        }

        public IList<T> GetPageWithInclude(Expression<Func<T, object>> predicat, int skip, int take)
        {
            return this.dbContext.Set<T>()
                .Include(predicat).Skip(skip).Take(take).ToList();
        }

        public IList<T> GetPage(
            Expression<Func<T, bool>> predicat,
            int skip, int take)
        {
            return this.dbContext.Set<T>()
                        .Where(predicat)
                        .Skip(skip).Take(take).ToList();
        }

        public bool Exist(Expression<Func<T, bool>> predicat)
        {
            return this.dbContext.Set<T>().Any(predicat);
        }

        public void Add(T entity)
        {
            this.dbContext.Set<T>().Add(entity);
            this.dbContext.SaveChanges();
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
            this.dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = this.dbContext.Set<T>().Find(id);
            this.dbContext.Set<T>().Remove(entity);
        }

        public T GetLastEntity<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy)
        {
            return this.dbContext.Set<T>().OrderBy(orderBy).Last();
        }

        public int Count()
        {
            return this.dbContext.Set<T>().Count();
        }
    }
}
