using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationPortal.BLL.Services
{
    public class LogInService : ILogInService
    {
        private readonly IRepository<User> userRepository;
        private readonly IWorkWithAuthorizedUser workWithAuthorizedUser;
        private readonly ILogger<LogInService> logger;

        public LogInService(
            IRepository<User> uRepo,
            IWorkWithAuthorizedUser workWithAuthUser,
            ILogger<LogInService> logger)
        {
            this.userRepository = uRepo;
            this.workWithAuthorizedUser = workWithAuthUser;
            this.logger = logger;
        }

        public async Task<bool> LogIn(string name, string password)
        {
             var user = await this.userRepository.GetOne(x => x.Name.ToLower() == name.ToLower() && x.Password == password);

             if (user == null)
            {
                this.logger.LogInformation($"User - ({name}) not found!");
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
