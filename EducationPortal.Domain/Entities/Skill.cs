namespace DataAccessLayer.Entities
{
    using EducationPortal.Domain.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;

    [XmlType("Skill")]
    public class Skill : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("CountOfPoint")]
        public int CountOfPoint { get; set; }

        [XmlIgnore]
        public ICollection<CourseSkill> CourseSkills { get; set; }

        [XmlIgnore]
        public ICollection<UserSkill> UserSkills { get; set; }
    }
}
