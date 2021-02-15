namespace Entities
{
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Serialization;

    [XmlType("Book")]
    public class Book : Material
    {
        [XmlElement("CountOfPages")]
        public int CountOfPages { get; set; }

        [XmlElement("Author")]
        public string Author { get; set; }

        public override string ToString()
        {
            return $"Type: Book" +
                $"\nName: {this.Name}" +
                $"\nCountOfPages: {this.CountOfPages}" +
                $"\nAuthor: {this.Author}";
        }
    }
}
