using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Linq;

namespace DataAccessLayer.Entities
{
    [XmlType("Course")]
    [XmlInclude(typeof(Skill)), XmlInclude(typeof(Material))]
    public class Course
    {
        [XmlElement("CourseID")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlArray("SkillArray")]
        [XmlArrayItem("SkillObjekt")]
        public List<Skill> Skills = new List<Skill>();

        [XmlArray("MaterialArray")]
        [XmlArrayItem("MaterialObjekt")]
        public List<Material> Materials = new List<Material>();

        public override string ToString()
        {
            return $"\nName: {Name}" +
                $"\nDescription: {Description}" +
                $"\nYou will acquire the following skills: { string.Join(",", Skills.Select(s => s.Name)) }" +
                $"\nThe course contains the following list of materials: { string.Join(",", Materials.Select(x => x.Name))}";
        }
    }
}
