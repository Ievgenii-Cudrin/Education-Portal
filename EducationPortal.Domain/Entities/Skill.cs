﻿namespace DataAccessLayer.Entities
{
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Serialization;

    [XmlType("Skill")]
    public class Skill : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("CountOfPoint")]
        public int CountOfPoint { get; set; }
    }
}
