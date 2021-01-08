using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Entities;
using DataAccessLayer.Serialization;

namespace DataAccessLayer.DataContext
{
    public class XmlSerializeContext
    {
        public XmlSet<User> Users = new XmlSet<User>();

        public XmlSet<Material> Materials = new XmlSet<Material>();

        public XmlSet<Skill> Skills = new XmlSet<Skill>();

        public XmlSet<Course> Courses = new XmlSet<Course>();
    }
}
