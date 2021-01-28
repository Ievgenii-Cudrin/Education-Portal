using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using XmlDataBase.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        IXmlSerializeContext<T> context;

        public Repository(IXmlSerializeContext<T> context)
        {
            this.context = context;
        }
        public void Create(T item)
        {
            context.XmlSet.Add(item);
        }

        public void Delete(int id)
        {
            context.XmlSet.Delete(id);
        }

        public T Get(int id)
        {
            return context.XmlSet.Get(id);
        }

        public IEnumerable<T> GetAll()
        {
            return context.XmlSet.GetAll();
        }

        public void Update(T item)
        {
            context.XmlSet.Update(item);
        }
    }
}
