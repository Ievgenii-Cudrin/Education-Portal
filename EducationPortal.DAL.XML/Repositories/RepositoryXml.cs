using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Interfaces;
using EducationPortal.Domain.Entities;
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

        public void Add(T item)
        {
            this.context.XmlSet.Add(item);
        }

        public void Delete(int id)
        {
            this.context.XmlSet.Delete(id);
        }

        public IList<T> Except(IList<T> list, IEqualityComparer<T> comparer)
        {
            return this.context.XmlSet.GetAll().Except(list, comparer).ToList();
        }

        public bool Exist(Expression<Func<T, bool>> predicat)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Any(predicat);
        }

        public T Get(int id)
        {
            return this.context.XmlSet.Get(id);
        }

        public virtual IList<TResult> Get<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicat)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Where(predicat)
                .Select(selector)
                .ToList();
        }

        public IList<TResult> Get<TResult>(Expression<Func<T, TResult>> selector)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Select(selector).ToList();
        }

        public IList<T> Get(Expression<Func<T, bool>> predicat)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Where(predicat).ToList();
        }

        public T GetOne(Expression<Func<T, bool>> predicat)
        {
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Where(predicat).Take(1).FirstOrDefault();
        }

        public IList<T> GetAll()
        {
            return this.context.XmlSet.GetAll().ToList();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return this.GetAll();
        }

        public T GetLastEntity<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy)
        {
            return this.context.XmlSet.GetAll().AsQueryable().OrderBy(orderBy).Last();
        }

        public IList<T> GetPage(Expression<Func<T, bool>> predicat, PageInfo page)
        {
            var skip = page.Size * (page.Number - 1);
            return this.context.XmlSet.GetAll()
                .AsQueryable()
                .Where(predicat)
                .Skip(skip)
                .Take(page.Size).ToList();
        }

        public IList<T> GetPage(PageInfo page)
        {
            var skip = page.Size * (page.Number - 1);
            return this.context.XmlSet.GetAll()
                .Skip(skip)
                .Take(page.Size).ToList();
        }

        public void Save()
        {
            //Save document
        }

        public void Update(T item)
        {
            this.context.XmlSet.Update(item);
        }
    }
}
