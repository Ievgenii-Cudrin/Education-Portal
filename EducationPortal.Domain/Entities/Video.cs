using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
using EducationPortal.Domain.Enums;

namespace Entities
{
    [XmlType("Video")]
    public class Video : Material
    {
        [XmlElement("Link")]
        public string Link { get; set; }

        [XmlElement("Quality")]
        public VideoQuality Quality { get; set; }

        [XmlElement("Duration")]
        public int Duration { get; set; }

        public override string ToString()
        {
            return $"Type: Video" +
                $"\nName: {this.Name}" +
                $"\nLink: {this.Link}" +
                $"\nVideo quality: {this.Quality}" +
                $"\nVideo duration: {this.Duration}";
        }
    }
}
