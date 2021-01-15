using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Interfaces
{
    public interface IUserController
    {
        public void CreateNewUser();

        public void VerifyLoginAndPassword();

        void ShowAllUser();
    }
}
