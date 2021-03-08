namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using DataAccessLayer.Entities;

    public interface ILogInService
    {
        Task<bool> LogIn(string email, string password);

        bool LogOut();
    }
}
