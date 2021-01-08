using DataAccessLayer.DataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private XmlSerializeContext _context;
        public UserRepository(XmlSerializeContext context)
        {
            this._context = context;
        }
        public void Create(User item)
        {
            _context.Users.Add(item);
        }

        public void Delete(int id)
        {
            _context.Users.Delete(id);
        }

        public User Get(int id)
        {
            return _context.Users.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.GetAll();
        }

        public void Update(User item)
        {
            _context.Users.UpdateObject(item);
        }
    }
}
