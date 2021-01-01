using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataAccessLayer.Entities
{
    [XmlType("Book")]
    public class Book : Material
    {
        [XmlElement("CountOfPages")]
        public int CountOfPages { get; set; }

        [XmlElement("Author")]
        public string Author { get; set; }
    }
}
