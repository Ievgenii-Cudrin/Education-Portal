namespace EducationPortal.Domain.Entities
{
    using DataAccessLayer.Entities;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class UserCourse : BaseEntity
    {
        [XmlIgnore]
        public int UserId { get; set; }

        [XmlIgnore]
        public User User { get; set; }

        [XmlIgnore]
        public int CourseId { get; set; }

        [XmlIgnore]
        public Course Course { get; set; }

        [XmlIgnore]
        public bool IsPassed { get; set; }

        [XmlIgnore]
        public ICollection<UserCourseMaterial> UserCourseMaterials { get; set; }
    }
}
