namespace DataAccessLayer.DataContext
{
    using XmlDataBase.Interfaces;

    public class XmlSerializationContextGeneric<T> : IXmlSerializeContext<T>
        where T : class
    {
        private readonly IXmlSet<T> xmlSet;

        public XmlSerializationContextGeneric(IXmlSet<T> xmlSet)
        {
            this.xmlSet = xmlSet;
        }

        public IXmlSet<T> XmlSet
        {
            get { return this.xmlSet; }
        }
    }
}
