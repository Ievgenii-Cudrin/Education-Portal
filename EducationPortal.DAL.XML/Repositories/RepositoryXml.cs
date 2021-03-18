using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using XmlDataBase.Interfaces;

namespace EducationPortal.DAL.XML.Repositories
{
    public class RepositoryXml<T> : IRepository<T>
        where T : class
    {
        private readonly IXmlSerializeContext<T> context;

        public RepositoryXml(IXmlSerializeContext<T> context)
        {
            this.context = context;
        }

        public async Task Add(T item)
        {
            this.context.XmlSet.Add(item);
        }

        public async Task Delete(int id)
        {
            this.context.XmlSet.Delete(id);
        }

        public async Task<bool> Exist(Expression<Func<T, bool>> predicat)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Any(predicat);
        }

        public async Task<T> Get(int id)
        {
            return this.context.XmlSet.Get(id);
        }

        public virtual async Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicat)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Where(predicat)
                .Select(selector)
                .ToList();
        }

        public async Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<T, TResult>> selector)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Select(selector).ToList();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicat)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Where(predicat).ToList();
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> predicat)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Where(predicat).Take(1).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return this.context.XmlSet.GetAll().ToList();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetLastEntity<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy)
        {
            return this.context.XmlSet.GetAll().AsQueryable().OrderBy(orderBy).Last();
        }

        public async Task<IEnumerable<T>> GetPage(Expression<Func<T, bool>> predicat, int take, int skip)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Where(predicat)
                .Skip(skip)
                .Take(skip).ToList();
        }

        public async Task<IEnumerable<T>> GetPage(int skip, int take)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Skip(skip)
                .Take(take).ToList();
        }

        public async Task Save()
        {
            //Save document
        }

        public async Task Update(T item)
        {
            this.context.XmlSet.Update(item);
        }

        public async Task<int> Count()
        {
            return this.context.XmlSet.GetAll().Count();
        }

        public async Task<IEnumerable<T>> GetPageWithInclude(Expression<Func<T, object>> predicat, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(T entity)
        {
            int id = (int)typeof(T).GetProperty("Id").GetValue(entity);
            await this.Delete(id);
        }

        public async Task<int> GetCountWithPredicate(Expression<Func<T, bool>> predicat)
        {
            return this.context.XmlSet.GetAll().AsQueryable().Where(predicat).Count();
        }

        public virtual async Task<IEnumerable<T>> GetWithInclude(Expression<Func<T, bool>> predicat, Expression<Func<T, object>> param)
        {
            throw new NotImplementedException();
        }
    }
}
