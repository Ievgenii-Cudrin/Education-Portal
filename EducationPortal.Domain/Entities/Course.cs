namespace DataAccessLayer.Entities
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Xml.Serialization;
    using global::Entities;

    [XmlType("Course")]
    [XmlInclude(typeof(Skill))]
    [XmlInclude(typeof(Material))]
    public class Course : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlArray("SkillArray")]
        [XmlArrayItem("SkillObjekt")]
        public List<Skill> Skills { get; set; }

        [XmlArray("MaterialArray")]
        [XmlArrayItem("MaterialObjekt")]
        public List<Material> Materials { get; set; }

        public override string ToString()
        {
            return $"\nName: {this.Name}" +
                $"\nDescription: {this.Description}" +
                $"\nYou will acquire the following skills: {string.Join(",", this.Skills.Select(s => s.Name))}" +
                $"\nThe course contains the following list of materials: {string.Join(",", this.Materials.Select(x => x.Name))}";
        }
    }
}
