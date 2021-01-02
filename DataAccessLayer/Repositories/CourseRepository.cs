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
        private XmlSerializeContext _context;
        public CourseRepository(XmlSerializeContext context)
        {
            this._context = context;
        }
        public void Create(Course item)
        {
            _context.Courses.Add(item);
        }

        public void Delete(int id)
        {
            _context.Courses.Delete(id);
        }

        public Course Get(int id)
        {
            return _context.Courses.Get(id);
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.GetAll();
        }

        public void Update(Course item)
        {
            _context.Courses.UpdateObject(item);
        }
    }
}
