using BusinessLogicLayer.Interfaces;
using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
using EducationPortalConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationPortalConsoleApp.Controller
{
    public class UserController : IUserController
    {
        IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public void VerifyLoginAndPassword()
        {
            string name = GetDataHelper.GetNameFromUser();
            string password = GetDataHelper.GetPasswordFromUser();
            //verify user
            bool validUser = userService.VerifyUser(name, password);

            if (validUser)
            {
                Console.WriteLine("Authorization passed");
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
            else
            {
                Console.WriteLine("User with such data does not exist");
                ProgramBranch.StartApplication();
            }
        }

        public void CreateNewUser()
        {
            string name = GetDataHelper.GetNameFromUser();
            string password = GetDataHelper.GetPasswordWithConfirmFromUser();
            string phoneNumber = GetDataHelper.GetPhoneNumberFromUser();
            string email = GetDataHelper.GetEmailFromUser();
            //Create new user, if not - false
            bool createUser = userService.CreateUser(name, password, email, phoneNumber);

            if(createUser)
            {
                Console.WriteLine("User successfully created!");
            }
            else
            {
                Console.WriteLine("Something wrong");
                ProgramBranch.StartApplication();
            }
        }

        public void ShowAllUser()
        {
            Dictionary<int, string> users = userService.GetAllUsers();

            foreach (var user in users.ToArray())
            {
                users[user.Key] = user.Value.Trim();
            }
                
        }
    }
}
