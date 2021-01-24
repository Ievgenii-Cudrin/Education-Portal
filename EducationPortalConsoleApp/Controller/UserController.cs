using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.InstanceCreator;
using EducationPortal.PL.Mapping;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
using EducationPortalConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
            else
            {
                Console.WriteLine("User with such data does not exist");
                Thread.Sleep(4000);
                ProgramBranch.StartApplication();
            }
        }

        public void CreateNewUser()
        {
            Console.Clear();
            UserViewModel userVM = UserVMInstanceCreator.CreateUser();

            if(userService.GetAllUsers().Any(x => x.Email == userVM.Email))
            {
                Console.WriteLine("User with this email already exists!");
                Thread.Sleep(4000);
                ProgramBranch.StartApplication();
            }
            //Create new user, if not - false
            bool createUser = userService.CreateUser(Mapping.CreateMapFromVMToDomain<UserViewModel, User>(userVM));
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
            IEnumerable<User> users = userService.GetAllUsers();

            //TODO HERE
        }

        public void UpdateUser()
        {
            //TODO HERE
            Console.Clear();
            UserViewModel userToUpdate = UserVMInstanceCreator.CreateUser();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, User>());
            var mapper = new Mapper(config);
            // сопоставление
            var userMap = mapper.Map<UserViewModel, User>(userToUpdate);
            //Create new user, if not - false
            bool updateUser = userService.UpdateUser(userMap);

            ProgramConsoleMessageHelper.
                ShowFunctionResult(
                updateUser,
                "User successfully updated!",
                "Something wrong",
                ProgramBranch.SelectFirstStepForAuthorizedUser,
                ProgramBranch.SelectFirstStepForAuthorizedUser
                );
        }

        public void LogOut()
        {
            userService.LogOut();
        }
    }
}
