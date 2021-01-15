using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.InstanceCreator
{
    public static class CourseInstanceCreator
    {
        public static Course CourseCreator(string name, string description)
        {
            Course user = null;

            if (name != null && description != null)
            {
                user = new Course()
                {
                    Name = name,
                    Description = description,
                    Materials = new List<Material>(),
                    Skills = new List<Skill>()
                };
            }

            return user == null ? null : user;
        }
    }
}
