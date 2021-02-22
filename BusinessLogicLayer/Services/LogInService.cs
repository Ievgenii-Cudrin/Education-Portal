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
        private IWorkWithAuthorizedUser workWithAuthorizedUser;

        public LogInService(IEnumerable<IRepository<User>> uRepo, IWorkWithAuthorizedUser workWithAuthUser)
        {
            this.userRepository = uRepo.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<User>));
            this.workWithAuthorizedUser = workWithAuthUser;
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
                this.workWithAuthorizedUser.SetUser(user);
                return true;
            }
        }

        public bool LogOut()
        {
            this.workWithAuthorizedUser.CleanUser();
            return true;
        }
    }
}
