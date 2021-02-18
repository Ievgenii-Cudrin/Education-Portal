namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataAccessLayer.Entities;

    public interface ILogInService
    {
        public bool LogIn(string email, string password);

        public bool LogOut();

        public User AuthorizedUser { get; }
    }
}
