using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataAccessLayer.Entities
{
    public class BaseEntity
    {
        [XmlElement("Id")]
        public int Id { get; set; }
<<<<<<< HEAD
=======

>>>>>>> main
    }
}
