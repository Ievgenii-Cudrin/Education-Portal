namespace XmlDataBase.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IXmlSet<T> 
        where T : class
    {
        void Add(T objToXml);

        IEnumerable<T> GetAll();

        T Get(int id);

        void Delete(int id);

        void Update(T objectToUpdate);
    }
}
