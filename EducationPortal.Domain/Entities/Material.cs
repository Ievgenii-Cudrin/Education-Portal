﻿namespace Entities
{
    using System.Xml.Serialization;
    using DataAccessLayer.Entities;

    [XmlType("Material")] // define Type
    [XmlInclude(typeof(Video))]
    [XmlInclude(typeof(Book))]
    [XmlInclude(typeof(Article))]
    public class Material : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IsPassed")]
        public bool IsPassed { get; set; }
    }
}