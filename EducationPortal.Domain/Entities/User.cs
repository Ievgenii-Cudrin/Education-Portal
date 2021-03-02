﻿namespace DataAccessLayer.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;
    using EducationPortal.Domain.Entities;
    using global::Entities;

    [XmlType("User")]
    [XmlInclude(typeof(Skill))]
    [XmlInclude(typeof(Course))]
    public class User : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Password")]
        public string Password { get; set; }

        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlIgnore]
        public List<UserSkill> UserSkills { get; set; }

        [XmlIgnore]
        public ICollection<UserCourse> UserCourses { get; set; }

        [XmlIgnore]
        public ICollection<UserMaterial> UserMaterials { get; set; }

        public User() { }
    }
}
