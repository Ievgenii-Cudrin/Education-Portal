namespace DataAccessLayer.Entities
{
    using System.Xml.Serialization;

    public class BaseEntity
    {
        [XmlElement("Id")]
        public int Id { get; set; }
    }
}
