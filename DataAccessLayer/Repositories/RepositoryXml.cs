namespace DataAccessLayer.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using DataAccessLayer.Interfaces;
    using XmlDataBase.Interfaces;

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

        public T Get(int id)
        {
            return this.context.XmlSet.Get(id);
        }

        public IList<T> GetAll()
        {
            return this.context.XmlSet.GetAll().ToList();
        }

        public void Update(T item)
        {
            this.context.XmlSet.Update(item);
        }
    }
}
