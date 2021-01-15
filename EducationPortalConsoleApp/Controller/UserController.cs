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

            ProgramConsoleMessageHelper
                .ShowFunctionResult(validUser,
                "Authorization passed",
                "User with such data does not exist",
                ProgramBranch.SelectFirstStepForAuthorizedUser,
                ProgramBranch.StartApplication);
            
        }

        public void CreateNewUser()
        {
            string name = GetDataHelper.GetNameFromUser();
            string password = GetDataHelper.GetPasswordWithConfirmFromUser();
            string phoneNumber = GetDataHelper.GetPhoneNumberFromUser();
            string email = GetDataHelper.GetEmailFromUser();
            //Create new user, if not - false
            bool createUser = userService.CreateUser(name, password, email, phoneNumber);
            //Show result
            ProgramConsoleMessageHelper.
                ShowFunctionResult(
                createUser,
                "User successfully created!",
                "Something wrong",
                ProgramBranch.StartApplication,
                ProgramBranch.StartApplication
                );
        }

        public void ShowAllUser()
        {
            Dictionary<int, string> users = userService.GetAllUsers();

            foreach (var user in users.ToArray())
            {
                Console.WriteLine($"Id: {user.Key}, Name: {user.Value}");
            }
        }
    }
}
