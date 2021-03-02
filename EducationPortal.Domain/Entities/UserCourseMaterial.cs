namespace EducationPortal.Domain.Entities
{
    using global::Entities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;

    [XmlType("UserCourseMaterial")]
    public class UserCourseMaterial
    {
        [NotMapped]
        public int Id { get; set; }

        [XmlElement("UserCourseId")]
        public int UserCourseId { get; set; }

        [XmlIgnore]
        public UserCourse UserCourse { get; set; }

        [XmlElement("MaterialId")]
        public int MaterialId { get; set; }

        [XmlIgnore]
        public Material Material { get; set; }

        [XmlElement("IsPassed")]
        public bool IsPassed { get; set; }
    }
}
