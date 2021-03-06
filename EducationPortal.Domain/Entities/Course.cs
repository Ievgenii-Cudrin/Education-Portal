﻿namespace DataAccessLayer.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Xml.Serialization;
    using EducationPortal.Domain.Entities;
    using global::Entities;

    [XmlType("Course")]
    [XmlInclude(typeof(Skill))]
    [XmlInclude(typeof(Material))]
    public class Course : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [NotMapped]
        [XmlArray("SkillArray")]
        [XmlArrayItem("SkillObjekt")]
        public ICollection<Skill> Skills { get; set; }

        [NotMapped]
        [XmlArray("MaterialArray")]
        [XmlArrayItem("MaterialObjekt")]
        public ICollection<Material> Materials { get; set; }

        [XmlIgnore]
        public ICollection<CourseSkill> CourseSkills { get; set; }

        [XmlIgnore]
        public ICollection<UserCourse> CourseUsers { get; set; }

        [XmlIgnore]
        public ICollection<CourseMaterial> CourseMaterials { get; set; }

        public override string ToString()
        {
            return $"\nName: {this.Name}" +
                $"\nDescription: {this.Description}" +
                $"\nYou will acquire the following skills: {string.Join(",", this.Skills.Select(s => s.Name))}" +
                $"\nThe course contains the following list of materials: {string.Join(",", this.Materials.Select(x => x.Name))}";
        }
    }
}
