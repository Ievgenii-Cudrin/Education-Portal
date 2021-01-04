using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataAccessLayer.Entities
{
    [XmlType("Material")] // define Type
    [XmlInclude(typeof(Video)), XmlInclude(typeof(Book)), XmlInclude(typeof(Article))]
    public class Material
    {
        [XmlElement("MaterialID")]
        public string Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }
    }
}
