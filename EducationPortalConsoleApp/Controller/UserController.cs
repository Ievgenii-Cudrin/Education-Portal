using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Controller
{
    public static class UserController
    {
        public static void GetLoginAndPassword()
        {

        }

        public static void CreateNewUser()
        {
            string name = GetDataHelper.GetNameFromUser();
            string password = GetDataHelper.GetPasswordFromUser();
            string phoneNumber = GetDataHelper.GetPhoneNumberFromUser();
            string email = GetDataHelper.GetEmailFromUser();

            bool createUser = 
        }
    }
}
