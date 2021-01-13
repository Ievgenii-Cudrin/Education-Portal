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
        public int Quality { get; set; }

        [XmlElement("Duration")]
        public int Duration { get; set; }

        public override string ToString()
        {
            return $"Type: Video" +
                $"\nName: {Name}" +
                $"\nLink: {Link}" +
                $"\nVideo quality: {Quality}" +
                $"\nVideo duration: {Duration}";
        }
    }
}