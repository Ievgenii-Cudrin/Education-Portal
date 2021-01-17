using System;
using System.Collections.Generic;
using System.Text;
using XmlDataBase.Interfaces;

namespace XmlDataBase.Interfaces
{
    public interface IXmlSerializeContext<T> where T : class
    {
        public IXmlSet<T> XmlSet { get; }
    }
}
