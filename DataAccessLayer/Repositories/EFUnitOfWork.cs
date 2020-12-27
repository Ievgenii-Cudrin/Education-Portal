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
        private UserContext context;
        private UserRepository userRepository;

        public EFUnitOfWork()
        {
            context = new UserContext();
        }

        public IRepository<User> Users
        {
            get
            {
                if(userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            context.Users.SaveChanges();
        }
    }
}
