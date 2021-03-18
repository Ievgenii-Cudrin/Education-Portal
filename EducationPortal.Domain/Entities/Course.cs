using System.Collections.Generic;
using System.Xml.Serialization;
using EducationPortal.Domain.Entities;
using global::Entities;

namespace DataAccessLayer.Entities
{
    [XmlType("Course")]
    [XmlInclude(typeof(Skill))]
    [XmlInclude(typeof(Material))]
    public class Course : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlIgnore]
        public ICollection<CourseSkill> CourseSkills { get; set; }

        [XmlIgnore]
        public ICollection<UserCourse> CourseUsers { get; set; }

        [XmlIgnore]
        public ICollection<CourseMaterial> CourseMaterials { get; set; }
    }
}
