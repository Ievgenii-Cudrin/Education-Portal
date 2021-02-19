namespace DataAccessLayer.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;
    using EducationPortal.Domain.Entities;

    [XmlType("Skill")]
    public class Skill : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [NotMapped]
        [XmlElement("CountOfPoint")]
        public int CountOfPoint { get; set; }

        [XmlIgnore]
        public ICollection<CourseSkill> CourseSkills { get; set; }

        [XmlIgnore]
        public ICollection<UserSkill> UserSkills { get; set; }
    }
}
