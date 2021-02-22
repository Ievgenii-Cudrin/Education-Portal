namespace EducationPortal.BLL.Services
{
    using DataAccessLayer.Entities;
    using EducationPortal.BLL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

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
