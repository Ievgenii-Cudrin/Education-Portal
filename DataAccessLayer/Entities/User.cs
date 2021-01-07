using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataAccessLayer.Entities
{
    [XmlType("User")]
    [XmlInclude(typeof(Skill)), XmlInclude(typeof(Course))]
    public class User
    {
        [XmlElement("UserID")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlArray("SkillArray")]
        [XmlArrayItem("SkillObjekt")]
        public List<Skill> Skills = new List<Skill>();

        [XmlArray("CourseArray")]
        [XmlArrayItem("SkillObjekt")]
        public List<Course> Courses = new List<Course>();

        public User() { }
    }
}
