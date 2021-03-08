namespace EducationPortal.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DataAccessLayer.Interfaces;
    using EducationPortal.DAL.DataContext;
    using Microsoft.EntityFrameworkCore;

    public class RepositorySql<T> : IRepository<T>
        where T : class
    {
        private readonly ApplicationContext dbContext;

        public RepositorySql(ApplicationContext context)
        {
            this.dbContext = context;
        }

        public async Task<IList<T>> GetAll()
        {
            return await this.dbContext.Set<T>().ToListAsync();
        }

        public async Task<IList<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var result = this.dbContext.Set<T>();
            foreach (var include in includes)
            {
                result.Include(include);
            }

            return await result.ToListAsync();
        }

        public async Task<IList<T>> Get(
            Expression<Func<T, bool>> predicat,
            params Expression<Func<T, object>>[] includes)
        {
            var result = this.dbContext.Set<T>();
            foreach (var include in includes)
            {
                result.Include(include);
            }

            return await result.Where(predicat).ToListAsync();
        }

        public async Task<IList<TResult>> Get<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await this.dbContext.Set<T>()
                        .Select(selector).ToListAsync();
        }

        public async Task<IList<TResult>> Get<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicat)
        {
            return this.dbContext.Set<T>()
                          .Where(predicat)
                          .Select(selector).ToList();
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> predicat)
        {
            return await this.dbContext.Set<T>()
                        .Where(predicat).Take(1).FirstOrDefaultAsync();
        }

        public async Task<IList<T>> Get(Expression<Func<T, bool>> predicat)
        {
            return await this.dbContext.Set<T>()
                        .Where(predicat).ToListAsync();
        }

        public async Task<IList<T>> GetPage(int skip, int take)
        {
            return await this.dbContext.Set<T>()
                        .Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IList<T>> GetPageWithInclude(Expression<Func<T, object>> predicat, int skip, int take)
        {
            return await this.dbContext.Set<T>()
                .Include(predicat).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IList<T>> GetPage(
            Expression<Func<T, bool>> predicat,
            int skip, int take)
        {
            return await this.dbContext.Set<T>()
                        .Where(predicat)
                        .Skip(skip).Take(take).ToListAsync();
        }

        public async Task<bool> Exist(Expression<Func<T, bool>> predicat)
        {
            return await this.dbContext.Set<T>().AnyAsync(predicat);
        }

        public async Task Add(T entity)
        {
            await this.dbContext.Set<T>().AddAsync(entity);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Save()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<T> Get(int id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }

        public async Task Update(T item)
        {
            this.dbContext.Entry<T>(item).State = EntityState.Modified;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await this.dbContext.Set<T>().FindAsync (id);
            this.dbContext.Set<T>().Remove (entity);
        }

        public async Task<T> GetLastEntity<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy)
        {
            return await this.dbContext.Set<T>().OrderBy(orderBy).LastAsync();
        }

        public async Task<int> Count()
        {
            return await this.dbContext.Set<T>().CountAsync();
        }
    }
}
