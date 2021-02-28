namespace EducationPortal.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.DAL.DataContext;
    using EducationPortal.DAL.Interfaces;
    using EducationPortal.Domain.Entities;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using NLog;

    public class RepositorySql<T> : IRepository<T>
        where T : class
    {
        private readonly ApplicationContext dbContext;
        private static IDalSqlLogger logger;

        public RepositorySql(IDalSqlLogger log, ApplicationContext context)
        {
            logger = log;
            this.dbContext = context;
        }

        public IList<T> GetAll()
        {
            try
            {
                return this.dbContext.Set<T>().ToList();
            }
            catch(SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " +ex.Message);
                return null;
            }
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var result = this.dbContext.Set<T>();
                foreach (var include in includes)
                {
                    result.Include(include);
                }

                return result.ToList();
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return null;
            }
        }

        public IList<T> Get(
            Expression<Func<T, bool>> predicat,
            params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var result = this.dbContext.Set<T>();
                foreach (var include in includes)
                {
                    result.Include(include);
                }

                return result.Where(predicat).ToList();
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return null;
            }
        }

        public IList<TResult> Get<TResult>(Expression<Func<T, TResult>> selector)
        {
            try
            {
                return this.dbContext.Set<T>()
                        .Select(selector).ToList();
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return null;
            }
        }

        public IList<TResult> Get<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicat)
        {
            try
            {
                return this.dbContext.Set<T>()
                        .Where(predicat)
                        .Select(selector).ToList();
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return null;
            }
        }

        public IList<T> Get(Expression<Func<T, bool>> predicat)
        {
            try
            {
                return this.dbContext.Set<T>()
                        .Where(predicat).ToList();
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return null;
            }
        }

        public IList<T> GetPage(PageInfo page)
        {
            try
            {
                var skip = page.Size * (page.Number - 1);
                return this.dbContext.Set<T>()
                            .Skip(skip).Take(page.Size).ToList();
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return null;
            }
        }

        public IList<T> GetPage(
            Expression<Func<T, bool>> predicat,
            PageInfo page)
        {
            try
            {
                var skip = page.Size * (page.Number - 1);
                return this.dbContext.Set<T>()
                            .Where(predicat)
                            .Skip(skip).Take(page.Size).ToList();
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return null;
            }
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
            try
            {
                return this.dbContext.Set<T>().Any(predicat);
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return false;
            }
        }

        public void Add(T entity)
        {
            try
            {
                this.dbContext.Set<T>().Add(entity);
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
            }
        }

        public void Save()
        {
            try
            {
                this.dbContext.SaveChanges();
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
            }
        }

        public T Get(int id)
        {
            try
            {
                return this.dbContext.Set<T>().Find(id);
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return null;
            }
        }

        public void Update(T item)
        {
            try
            {
                this.dbContext.Entry<T>(item).State = EntityState.Modified;
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = this.dbContext.Set<T>().Find(id);

                if (entity != null)
                {
                    this.dbContext.Set<T>().Remove(entity);
                }
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
            }
        }

        public IList<T> Except(IList<T> list, IEqualityComparer<T> comparer)
        {
            try
            {
                return this.dbContext.Set<T>().ToList().Except(list, comparer).ToList();
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return null;
            }
        }

        public T GetLastEntity<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy)
        {
            try
            {
                return this.dbContext.Set<T>().OrderBy(orderBy).Last();
            }
            catch (SqlException ex)
            {
                logger.Logger.Error("Cant connect to DB - " + DateTime.Now + " " + ex.Message);
                return null;
            }
        }
    }
}
