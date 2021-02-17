namespace XmlDataBase.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using XmlDataBase.Interfaces;

    public interface IXmlSerializeContext<T> 
        where T : class
    {
        IXmlSet<T> XmlSet { get; }
    }
}
