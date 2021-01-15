using EducationPortalConsoleApp.Controller;
using EducationPortalConsoleApp.DependencyInjection;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Branch
{
    public static class ProgramBranch
    {
        static IUserController userController = ProviderServicePL.Provider.GetRequiredService<IUserController>();

        public static void StartApplication()
        {
            ProgramConsoleMessageHelper.ShowTextForLoginOrRegistration();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    userController.VerifyLoginAndPassword();
                    //TODO add user functional
                    break;
                case "2":
                    userController.CreateNewUser();
                    StartApplication();
                    break;
                case "3":
                    userController.ShowAllUser();
                    StartApplication();
                    break;
                default:
                    Console.WriteLine("Default case");
                    StartApplication();
                    break;
            }
        }

        public static void SelectFirstStepForAuthorizedUser()
        {
            ProgramConsoleMessageHelper.ShowTextForFirstStepForAuthorizedUser();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    //materialController.CreateNewMaterial();
                    //courseController.CreateNewCourse();
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
