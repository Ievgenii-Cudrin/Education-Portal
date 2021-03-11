using DataAccessLayer.Entities;
using global::Entities;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationPortal.Domain.Entities
{
    [XmlType("CourseMaterial")]
    public class CourseMaterial
    {
        [NotMapped]
        public int Id { get; set; }

        [XmlElement("CourseId")]
        public int CourseId { get; set; }

        [XmlIgnore]
        public Course Course { get; set; }

        [XmlElement("MaterialId")]
        public int MaterialId { get; set; }

        [XmlIgnore]
        public Material Material { get; set; }
    }
}
