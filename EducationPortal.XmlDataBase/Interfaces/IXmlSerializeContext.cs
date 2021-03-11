namespace XmlDataBase.Interfaces
{
    public interface IXmlSerializeContext<T>
        where T : class
    {
        IXmlSet<T> XmlSet { get; }
    }
}
