using DataAccessLayer.Entities;
using EducationPortal.BLL.Interfaces;

namespace EducationPortal.BLL.Services
{
    public class AuthorizerUser : IWorkWithAuthorizedUser
    {
        static User authorizedUser;

        public User User
        {
            get
            {
                return authorizedUser;
            }
        }

        public void CleanUser()
        {
            authorizedUser = null;
        }

        public void SetUser(User user)
        {
            authorizedUser = user;
        }
    }
}
