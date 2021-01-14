using EducationPortalConsoleApp.Controller;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Branch
{
    public class ProgrammBranch : IProgramBranch
    {
        //Add dependency injection
        UserController userController = new UserController();
        MaterialController materialController = new MaterialController();
        CourseController courseController = new CourseController();
        SkillController skillController = new SkillController();
        public void StartApplication()
        {
            ProgramConsoleMessageHelper.ShowTextForLoginOrRegistration();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    userController.VerifyLoginAndPassword();
                    break;
                case "2":
                    userController.CreateNewUser();
                    break;
                default:
                    Console.WriteLine("Default case");
                    StartApplication();
                    break;
            }
        }

        public void SelectFirstStepForAuthorizedUser()
        {
            ProgramConsoleMessageHelper.ShowTextForFirstStepForAuthorizedUser();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    //materialController.CreateNewMaterial();
                    courseController.CreateNewCourse();
                    break;
                case "2":

                    break;
                case "3":

                    break;
                case "4":

                    break;
                case "5":

                    break;
                case "6":
                    StartApplication();
                    break;
                default:
                    Console.WriteLine("Default case");
                    StartApplication();
                    break;
            }
        }

        public static void StartWorkWithUser()
        {
            //UserConsoleMessageHelper.ShowTextForChoice();

            //string userChoice = Console.ReadLine();

            //switch (userChoice)
            //{
            //    case "1":
            //        CreateUser();
            //        break;
            //    case "2":
            //        UpdateUser();
            //        break;
            //    case "3":
            //        ShowAllUsers();
            //        break;
            //    case "4":
            //        DeleteUser();
            //        break;
            //    case "5":
            //        ProgramService.SelectEntityToWork();
            //        break;
            //    default:
            //        Console.WriteLine("Default case");
            //        break;
            //}
        }

    }
}
