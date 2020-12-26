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
        private UserContext _context;
        public UserRepository(UserContext context)
        {
            this._context = context;
        }
        public void Create(User item)
        {
            _context.Users.AddObjectToXml(item);
        }

        public void Delete(int id)
        {
            _context.Users.DeleteUserFromXml(id);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _context.Users.Find(predicate);
        }

        public User Get(int id)
        {
            return _context.Users.GetSingleUserFromXml(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.GetAllUsersFromXml();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
