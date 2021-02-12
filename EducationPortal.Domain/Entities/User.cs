﻿namespace DataAccessLayer.Entities
{
    using global::Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Serialization;

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

        [XmlArray("SkillArray")]
        [XmlArrayItem("SkillObjekt")]
        public ICollection<Skill> Skills { get; set; }

        [XmlArray("PassedCoursesArray")]
        [XmlArrayItem("PassedCourseObjekt")]
        public ICollection<Course> Courses { get; set; }

        public ICollection<Material> Materials { get; set; }

        [NotMapped]
        [XmlArray("InProgressCoursesArray")]
        [XmlArrayItem("InProgressCourseObjekt")]
        public ICollection<Course> CoursesInProgress { get; set; }
    }
}
