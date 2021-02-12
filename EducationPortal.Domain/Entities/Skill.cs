namespace DataAccessLayer.Entities
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("Skill")]
    public class Skill : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("CountOfPoint")]
        public int CountOfPoint { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
