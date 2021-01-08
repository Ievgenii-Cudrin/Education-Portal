using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataAccessLayer.Entities
{
    [XmlType("Article")]
    public class Article : Material
    {
        [XmlElement("PublicationDate")]
        public DateTime PublicationDate { get; set; }

        [XmlElement("Site")]
        public string Site { get; set; }
    }
}
