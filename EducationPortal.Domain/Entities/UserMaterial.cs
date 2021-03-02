namespace EducationPortal.Domain.Entities
{
    using DataAccessLayer.Entities;
    using global::Entities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;

    [XmlType("UserSkill")]
    public class UserMaterial
    {
        [NotMapped]
        public int Id { get; set; }

        [XmlElement("UserId")]
        public int UserId { get; set; }

        [XmlIgnore]
        public User User { get; set; }

        [XmlElement("MaterialId")]
        public int MaterialId { get; set; }

        [XmlIgnore]
        public Material Material { get; set; }
    }
}
