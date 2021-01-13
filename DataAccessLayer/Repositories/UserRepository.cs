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
        private XmlSerializeContext context;
        public UserRepository(XmlSerializeContext context)
        {
            this.context = context;
        }
        public void Create(User item)
        {
            context.Users.Add(item);
        }

        public void Delete(int id)
        {
            context.Users.Delete(id);
        }

        public User Get(int id)
        {
            return context.Users.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.GetAll();
        }

        public void Update(User item)
        {
            context.Users.UpdateObject(item);
        }
    }
}
