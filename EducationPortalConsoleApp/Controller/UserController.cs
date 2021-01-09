using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Controller
{
    public class UserController
    {
        static UserService service;
        public UserController()
        {
            service = new UserService();
        }
        public static void GetLoginAndPassword()
        {

        }

        public static void CreateNewUser()
        {
            string name = GetDataHelper.GetNameFromUser();
            string password = GetDataHelper.GetPasswordFromUser();
            string phoneNumber = GetDataHelper.GetPhoneNumberFromUser();
            string email = GetDataHelper.GetEmailFromUser();

            bool createUser = service.CreateUser(name, password, email, phoneNumber);

            if(createUser)
                Console.WriteLine("User successfully created!");
            else
                Console.WriteLine("Somthing wrong");
        }
    }
}
