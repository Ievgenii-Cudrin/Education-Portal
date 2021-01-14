using DataAccessLayer.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IXmlSerializeContext<T> where T : class
    {
        public IXmlSet<T> XmlSet { get; }
    }
}
