namespace EducationPortal.Domain.Entities
{
    using DataAccessLayer.Entities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;

    [XmlType("UserSkill")]
    public class UserSkill
    {
        [NotMapped]
        public int Id { get; set; }

        [XmlElement("UserId")]
        public int UserId { get; set; }

        [XmlIgnore]
        public User User { get; set; }

        [XmlElement("SkillId")]
        public int SkillId { get; set; }

        [XmlIgnore]
        public Skill Skill { get; set; }

        [XmlElement("CountOfPoint")]
        public int CountOfPoint { get; set; }
    }
}
