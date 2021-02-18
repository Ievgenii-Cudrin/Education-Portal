namespace EducationPortal.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;

    public class LogInService : ILogInService
    {
        private IRepository<User> userRepository;
        private IUserService userService;
        private static User authorizedUser;

        public User AuthorizedUser
        {
            get
            {
                if (authorizedUser != null)
                {
                    return authorizedUser;
                }

                return null;
            }
        }

        public LogInService(IEnumerable<IRepository<User>> uRepo, IUserService userServ)
        {
            this.userRepository = uRepo.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<User>));
            this.userService = userServ;
        }

        public bool LogIn(string name, string password)
        {
            User user = this.userRepository.Get(x => x.Name.ToLower() == name.ToLower() && x.Password == password).FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                authorizedUser = user;
            }

            return true;
        }

        public bool LogOut()
        {
            authorizedUser = null;
            return true;
        }
    }
}
