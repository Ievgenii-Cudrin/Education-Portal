using System;
using System.Collections.Generic;
using System.Text;

namespace XmlDataBase.Interfaces
{
    public interface IXmlSet<T> where T : class
    {
        public void Add(T objToXml);

        public IEnumerable<T> GetAll();

        public T Get(int id);

        public void Delete(int id);

        public void Update(T objectToUpdate);
    }
}
