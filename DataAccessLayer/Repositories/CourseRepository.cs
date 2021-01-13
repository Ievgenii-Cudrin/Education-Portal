using DataAccessLayer.DataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private XmlSerializeContext context;
        public CourseRepository(XmlSerializeContext context)
        {
            this.context = context;
        }
        public void Create(Course item)
        {
            context.Courses.Add(item);
        }

        public void Delete(int id)
        {
            context.Courses.Delete(id);
        }

        public Course Get(int id)
        {
            return context.Courses.Get(id);
        }

        public IEnumerable<Course> GetAll()
        {
            return context.Courses.GetAll();
        }

        public void Update(Course item)
        {
            context.Courses.UpdateObject(item);
        }
    }
}
