﻿using System;
using System.Xml.Serialization;
using Entities;

[XmlType("Article")]
public class Article : Material
{
    [XmlElement("PublicationDate")]
    public DateTime PublicationDate { get; set; }

    [XmlElement("Site")]
    public string Site { get; set; }

    public override string ToString()
    {
        return $"Type: Article" +
            $"\nName: {this.Name}" +
            $"\nPublicationDate: {this.PublicationDate}" +
            $"\nSite: {this.Site}";
    }
}
