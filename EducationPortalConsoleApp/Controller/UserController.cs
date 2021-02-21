﻿// <auto-generated />
namespace EducationPortalConsoleApp.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.PL.InstanceCreator;
    using EducationPortal.PL.Interfaces;
    using EducationPortal.PL.Mapping;
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Branch;
    using EducationPortalConsoleApp.Helpers;
    using EducationPortalConsoleApp.Interfaces;
    using Entities;

    public class UserController : IUserController
    {
        private IUserService userService;
        private IMapperService mapperService;
        private ILogInService logInService;
        private IAuthorizedUser authorizedUser;

        public UserController(IUserService userService,
            IMapperService mapper,
            ILogInService logInServ,
            IAuthorizedUser authorizedUser)
        {
            this.userService = userService;
            this.mapperService = mapper;
            this.logInService = logInServ;
            this.authorizedUser = authorizedUser;
        }

        public void VerifyLoginAndPassword()
        {
            // get data from user
            string name = GetDataHelper.GetNameFromUser();
            string password = GetDataHelper.GetPasswordFromUser();

            // verify user
            bool validUser = this.logInService.LogIn(name, password);

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
            // create user
            UserViewModel userVM = UserVMInstanceCreator.CreateUser();
            var user = this.mapperService.CreateMapFromVMToDomain<UserViewModel, User>(userVM);
            // user data is valid?
            if (this.userService.ExistEmail(x => x.Email == user.Email))
            {
                Console.WriteLine("User with this email already exists!");
                Thread.Sleep(4000);
                ProgramBranch.StartApplication();
            }

            // Create new user, if not - false
            bool createUser = this.userService.CreateUser(user);

            // Show result
            ProgramConsoleMessageHelper.
                ShowFunctionResult(
                createUser,
                "User successfully created!",
                "Something wrong",
                ProgramBranch.StartApplication,
                ProgramBranch.StartApplication
                );
        }

        public void LogOut()
        {
            this.logInService.LogOut();
        }

        public void ShowAllPassedCourses()
        {
            //get all passed courses from bll and mapping
            List<CourseViewModel> passedCourses = this.mapperService.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(this.userService.GetAllPassedCourseFromUser());

            if (passedCourses.Count == 0)
            {
                this.ShowMessageIfCountOfCourseListIsZero("Нou do not have passed courses");
            }

            ProgramConsoleMessageHelper.ShowCourseAndReturnMethod(passedCourses);
        }

        public void ShowAllCourseInProggres()
        {
            // get courses in progress from bll and mapping
            List<CourseViewModel> coursesInProgress = this.mapperService.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(this.userService.GetListWithCoursesInProgress());

            if (coursesInProgress.Count == 0)
            {
                this.ShowMessageIfCountOfCourseListIsZero("Нou do not have started courses");
            }

            ProgramConsoleMessageHelper.ShowCourseAndReturnMethod(coursesInProgress);
        }

        private void ShowMessageIfCountOfCourseListIsZero(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(4000);
            ProgramBranch.SelectFirstStepForAuthorizedUser();
        }

        public void ShowAllUserSkills()
        {
            Console.Clear();
            // get user skills from bll and mapping
            List<SkillViewModel> userSkills = this.mapperService.CreateListMap<Skill, SkillViewModel>(this.userService.GetAllUserSkills());

            if (userSkills == null)
            {
                Console.WriteLine("You don't have skills yet!");
                Thread.Sleep(4000);
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }

            // show skills
            for (int i = 0; i < userSkills.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{userSkills[i].Name}. Count of points - {userSkills[i].CountOfPoint}");
            }

            ProgramConsoleMessageHelper.ReturnMethod();
        }

        public void ShowUserInfo()
        {
            Console.Clear();

            //show user info
            Console.WriteLine($"Name - {this.authorizedUser.User.Name}\n" +
                $"Phone Number - {this.authorizedUser.User.PhoneNumber}\n" +
                $"Email - {this.authorizedUser.User.Email}\n");

            ProgramConsoleMessageHelper.ReturnMethod();
        }
    }
}
