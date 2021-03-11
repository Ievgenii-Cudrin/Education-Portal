using DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace EducationPortal.Domain.Entities
{
    [XmlType("CourseSkill")]
    public class CourseSkill
    {
        [NotMapped]
        public int Id { get; set; }

        [XmlElement("CourseId")]
        public int CourseId { get; set; }

        [XmlIgnore]
        public Course Course { get; set; }

        [XmlElement("SkillId")]
        public int SkillId { get; set; }

        [XmlIgnore]
        public Skill Skill { get; set; }
    }
}
