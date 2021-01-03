using DataAccessLayer.DataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private XmlSerializeContext context;
        private UserRepository userRepository;
        private MaterialRepository materialRepository;
        private CourseRepository courseRepository;
        private SkillRepository skillRepository;

        public EFUnitOfWork()
        {
            context = new XmlSerializeContext();
        }

        public IRepository<User> Users
        {
            get
            {
                if(userRepository == null)
                    userRepository = new UserRepository(context);
                return userRepository;
            }
        }

        public IRepository<Material> Materials
        {
            get
            {
                if (materialRepository == null)
                    materialRepository = new MaterialRepository(context);
                return materialRepository;
            }
        }

        public IRepository<Skill> Skills
        {
            get
            {
                if (skillRepository == null)
                    skillRepository = new SkillRepository(context);
                return skillRepository;
            }
        }

        public IRepository<Course> Courses
        {
            get
            {
                if (courseRepository == null)
                    courseRepository = new CourseRepository(context);
                return courseRepository;
            }
        }
    }
}
