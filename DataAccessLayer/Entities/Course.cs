using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataAccessLayer.Entities
{
    [XmlType("Course")]
    [XmlInclude(typeof(Skill)), XmlInclude(typeof(Material))]
    public class Course
    {
        [XmlElement("CourseID")]
        public int Id { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlArray("SkillArray")]
        [XmlArrayItem("SkillObjekt")]
        public List<Skill> Skills = new List<Skill>();

        [XmlArray("MaterialArray")]
        [XmlArrayItem("MaterialObjekt")]
        public List<Material> Materials = new List<Material>();
    }
}
