using System.Linq;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;

namespace EducationPortal.BLL.Services
{
    public class LogInService : ILogInService
    {
        private IRepository<User> userRepository;
        private IWorkWithAuthorizedUser workWithAuthorizedUser;

        public LogInService(IRepository<User> uRepo, IWorkWithAuthorizedUser workWithAuthUser)
        {
            this.userRepository = uRepo;
            this.workWithAuthorizedUser = workWithAuthUser;
        }

        public bool LogIn(string name, string password)
        {
             var user = this.userRepository.GetOne(x => x.Name.ToLower() == name.ToLower() && x.Password == password);

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
