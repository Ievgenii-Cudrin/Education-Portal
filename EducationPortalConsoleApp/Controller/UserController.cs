﻿using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.InstanceCreator;
using EducationPortal.PL.Mapping;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
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
            //get data from user
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
            //create user
            UserViewModel userVM = UserVMInstanceCreator.CreateUser();

            //user data is valid?
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

        public void LogOut()
        {
            userService.LogOut();
        }

        public void ShowAllPassedCourses()
        {
            //get all passed courses from bll and mapping
            List<CourseViewModel> passedCourses = Mapping.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(userService.AuthorizedUser.CoursesPassed);

            if (passedCourses.Count == 0)
            {
                ShowMessageIfCountOfCourseListIsZero("Нou do not have passed courses");
            }

            ProgramConsoleMessageHelper.ShowCourseAndReturnMethod(passedCourses);
        }

        public void ShowAllCourseInProggres()
        {
            //get courses in progress from bll and mapping
            List<CourseViewModel> coursesInProgress = Mapping.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(userService.AuthorizedUser.CoursesInProgress);

            if(coursesInProgress.Count == 0)
            {
                ShowMessageIfCountOfCourseListIsZero("Нou do not have started courses");
            }

            ProgramConsoleMessageHelper.ShowCourseAndReturnMethod(coursesInProgress);
        }

        void ShowMessageIfCountOfCourseListIsZero(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(4000);
            ProgramBranch.SelectFirstStepForAuthorizedUser();
        }
        
        public void ShowAllUserSkills()
        {
            Console.Clear();
            //get user skills from bll and mapping
            List<SkillViewModel> userSkills = Mapping.CreateListMap<Skill, SkillViewModel>(userService.GetAllUserSkills());

            if(userSkills == null)
            {
                Console.WriteLine("You don't have skills yet!");
                Thread.Sleep(4000);
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }

            //show skills
            for(int i = 0; i < userSkills.Count; i++)
            {
                Console.WriteLine($"{i+1}.{userSkills[i].Name}. Count of points - {userSkills[i].CountOfPoint}");
            }

            ProgramConsoleMessageHelper.ReturnMethod();
        }

        public void ShowUserInfo()
        {
            Console.Clear();
            //show user info
            Console.WriteLine($"Name - {userService.AuthorizedUser.Name}\n" +
                $"Phone Number - {userService.AuthorizedUser.PhoneNumber}\n" +
                $"Email - {userService.AuthorizedUser.Email}\n");

            ProgramConsoleMessageHelper.ReturnMethod();
        }
    }
}
