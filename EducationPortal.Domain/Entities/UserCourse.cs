namespace EducationPortal.Domain.Entities
{
    using DataAccessLayer.Entities;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("UserCourse")]
    public class UserCourse : BaseEntity
    {
        [XmlElement("UserId")]
        public int UserId { get; set; }

        [XmlIgnore]
        public User User { get; set; }

        [XmlElement("CourseId")]
        public int CourseId { get; set; }

        [XmlIgnore]
        public Course Course { get; set; }

        [XmlElement("IsPassed")]
        public bool IsPassed { get; set; }

        [XmlIgnore]
        public ICollection<UserCourseMaterial> UserCourseMaterials { get; set; }
    }
}
