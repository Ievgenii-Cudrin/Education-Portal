﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using DataAccessLayer.Entities;
using EducationPortal.Domain.Entities;

namespace Entities
{
    [XmlType("Material")] // define Type
    [XmlInclude(typeof(Video))]
    [XmlInclude(typeof(Book))]
    [XmlInclude(typeof(Article))]
    public class Material : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [NotMapped]
        [XmlElement("IsPassed")]
        public bool IsPassed { get; set; }

        [XmlIgnore]
        public ICollection<UserCourseMaterial> UserCourseMaterials { get; set; }

        [XmlIgnore]
        public ICollection<UserMaterial> UserMaterials { get; set; }

        [XmlIgnore]
        public ICollection<CourseMaterial> CourseMaterials { get; set; }
    }
}