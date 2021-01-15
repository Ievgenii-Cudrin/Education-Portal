using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataAccessLayer.Entities
{
    [XmlType("Skill")]
    public class Skill : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("CountOfPoint")]
        public int CountOfPoint { get; set; }
    }
}
