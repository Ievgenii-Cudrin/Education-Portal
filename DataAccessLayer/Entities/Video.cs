﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataAccessLayer.Entities
{
    [XmlType("Video")]
    public class Video : Material
    {
        [XmlElement("Link")]
        public string Link { get; set; }

        [XmlElement("Quality")]
        public string Quality { get; set; }

        [XmlElement("Duration")]
        public string Duration { get; set; }
    }
}