using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.BLL.Interfaces;
using EducationPortal.PL.InstanceCreator;
using EducationPortal.PL.Interfaces;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
using Entities;

namespace EducationPortalConsoleApp.Controller
{
    public class UserController : IUserController
    {
        private readonly IUserService userService;
        private readonly IMapperService mapperService;
        private readonly ILogInService logInService;
        private readonly IAuthorizedUser authorizedUser;
        private readonly IUserSkillSqlService userSkillService;
        private IApplication application;

        public UserController(IUserService userService,
            IMapperService mapper,
            ILogInService logInServ,
            IAuthorizedUser authorizedUser,
            IUserSkillSqlService userSkill)
        {
            this.userService = userService;
            this.mapperService = mapper;
            this.logInService = logInServ;
            this.authorizedUser = authorizedUser;
            this.userSkillService = userSkill;
        }

        public void WithApplication(IApplication application)
        {
            this.application = application;
        }

        public async Task VerifyLoginAndPassword()
        {
            // get data from user
            string name = GetDataHelper.GetNameFromUser();
            string password = GetDataHelper.GetPasswordFromUser();

            // verify user
            bool validUser = await this.logInService.LogIn(name, password);

            if (validUser)
            {
                await this.application.SelectFirstStepForAuthorizedUser();
            }
            else
            {
                Console.WriteLine("User with such data does not exist");
                Thread.Sleep(4000);
                await this.application.StartApplication();
            }
        }

        public async Task CreateNewUser()
        {
            Console.Clear();
            // create user
            UserViewModel userVM = UserVMInstanceCreator.CreateUser();
            var user = this.mapperService.CreateMapFromVMToDomain<UserViewModel, User>(userVM);

            // user data is valid?
            bool existUserWithThisEmail = await this.userService.ExistEmail(x => x.Email == user.Email);

            if (existUserWithThisEmail)
            {
                Console.WriteLine("User with this email already exists!");
                Thread.Sleep(4000);
                await this.application.StartApplication();
            }

            // Create new user, if not - false
            bool createUser = await this.userService.CreateUser(user);

            if (createUser)
            {
                Console.WriteLine("User successfully created!");
            }
            else
            {
                Console.WriteLine("Something wrong");
            }

            await this.application.StartApplication();
        }

        public void LogOut()
        {
            this.logInService.LogOut();
        }

        public async Task ShowAllPassedCourses()
        {
            var passedUserCourse = await this.userService.GetAllPassedCourseFromUser();
            //get all passed courses from bll and mapping
            var passedCourses = this.mapperService.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>((List<Course>)passedUserCourse);

            if (passedCourses.Count == 0)
            {
                await this.ShowMessageIfCountOfCourseListIsZero("Now do not have passed courses");
            }

            ProgramConsoleMessageHelper.ShowCourseAndReturnMethod(this.application, passedCourses);
        }

        public async Task ShowAllCourseInProggres()
        {
            // get courses in progress from bll and mapping
            var listWithCourseInProgres = await this.userService.GetListWithCoursesInProgress();
            var coursesInProgress = this.mapperService.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>((List<Course>)listWithCourseInProgres);

            if (coursesInProgress.Count == 0)
            {
                await this.ShowMessageIfCountOfCourseListIsZero("Нou do not have started courses");
            }

            ProgramConsoleMessageHelper.ShowCourseAndReturnMethod(this.application, coursesInProgress);
        }

        private async Task ShowMessageIfCountOfCourseListIsZero(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(4000);
            await this.application.SelectFirstStepForAuthorizedUser();
        }

        public async Task ShowAllUserSkills()
        {
            Console.Clear();
            // get user skills from bll and mapping
            var userSkills = await this.userService.GetAllUserSkills();
            var userSkillsVM = this.mapperService.CreateListMap<Skill, SkillViewModel>((List<Skill>)userSkills);

            if (userSkillsVM == null)
            {
                Console.WriteLine("You don't have skills yet!");
                Thread.Sleep(4000);
                await this.application.SelectFirstStepForAuthorizedUser();
            }

            // show skills
            for (int i = 0; i < userSkillsVM.Count; i++)
            {
                var countOfPoit = await this.userSkillService.GetCountOfUserSkill(this.authorizedUser.User.Id, userSkillsVM[i].Id);
                Console.WriteLine($"{i + 1}.{userSkillsVM[i].Name}. Count of points - {countOfPoit}");
            }

            ProgramConsoleMessageHelper.ReturnMethod(this.application);
        }

        public void ShowUserInfo()
        {
            Console.Clear();

            //show user info
            Console.WriteLine($"Name - {this.authorizedUser.User.Name}\n" +
                $"Phone Number - {this.authorizedUser.User.PhoneNumber}\n" +
                $"Email - {this.authorizedUser.User.Email}\n");

            ProgramConsoleMessageHelper.ReturnMethod(this.application);
        }
    }
}
