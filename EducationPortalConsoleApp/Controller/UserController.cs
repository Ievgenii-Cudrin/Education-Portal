using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Controller
{
    public class UserController
    {
        UserService userService;
        ProgrammBranch programmBranch;

        public UserController()
        {
            userService = new UserService();
            programmBranch = new ProgrammBranch();
        }
        public void VerifyLoginAndPassword()
        {
            string name = GetDataHelper.GetNameFromUser();
            string password = GetDataHelper.GetPasswordFromUser();

            bool validUser = userService.VerifyUser(name, password);

            if (validUser)
            {
                Console.WriteLine("Authorization passed");
                //TODO Add method to work with entity
                programmBranch.SelectStepForAuthorizedUser();
            }
            else
            {
                Console.WriteLine("User with such data does not exist");
                programmBranch.StartApplication();
            }
                

            
        }

        public void CreateNewUser()
        {
            string name = GetDataHelper.GetNameFromUser();
            string password = GetDataHelper.GetPasswordWithConfirmFromUser();
            string phoneNumber = GetDataHelper.GetPhoneNumberFromUser();
            string email = GetDataHelper.GetEmailFromUser();

            bool createUser = userService.CreateUser(name, password, email, phoneNumber);

            if(createUser)
                Console.WriteLine("User successfully created!");
            else
                Console.WriteLine("Somthing wrong");

            programmBranch.StartApplication();
        }
    }
}
