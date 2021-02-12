namespace Entities
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using DataAccessLayer.Entities;
    using EducationPortal.Domain.Entities;

    [XmlType("Material")] // define Type
    [XmlInclude(typeof(Video))]
    [XmlInclude(typeof(Book))]
    [XmlInclude(typeof(Article))]
    public class Material : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IsPassed")]
        public bool IsPassed { get; set; }

        public ICollection<ShowCourseMaterial> ShowCourseMaterials { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}