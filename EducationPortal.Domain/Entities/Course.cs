using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessLayer.Entities
{
    [XmlType("Course")]
    [XmlInclude(typeof(Skill)), XmlInclude(typeof(Material))]
    public class Course : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlArray("SkillArray")]
        [XmlArrayItem("SkillObjekt")]
        public List<Skill> Skills = new List<Skill>();

        [XmlArray("MaterialArray")]
        [XmlArrayItem("MaterialObjekt")]
        public List<Material> Materials = new List<Material>();

        public override string ToString()
        {
            return $"\nName: {Name}" +
                $"\nDescription: {Description}" +
                $"\nYou will acquire the following skills: { string.Join(",", Skills.Select(s => s.Name)) }" +
                $"\nThe course contains the following list of materials: { string.Join(",", Materials.Select(x => x.Name))}";
        }
    }

    public class CourseComparer : IEqualityComparer<Course>
    {
        public bool Equals([AllowNull] Course x, [AllowNull] Course y)
        {
            if(x != null && y != null)
            {
                return x.Id == y.Id;
            }
            return false;
        }

        public int GetHashCode([DisallowNull] Course obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
