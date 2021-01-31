using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using XmlDataBase.Interfaces;

namespace DataAccessLayer.DataContext
{
    public class XmlSerializationContextGeneric<T> : IXmlSerializeContext<T> where T : class
    {
        IXmlSet<T> xmlSet;

        public XmlSerializationContextGeneric(IXmlSet<T> xmlSet)
        {
            this.xmlSet = xmlSet;
        }

        public IXmlSet<T> XmlSet
        {
            get { return xmlSet; }
        }

        
    }
}
