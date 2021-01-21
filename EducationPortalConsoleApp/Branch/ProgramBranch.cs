using DataAccessLayer.Entities;
using EducationPortal.PL.Interfaces;
using EducationPortal.PL.Models;
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
        static ICourseController courseController = ProviderServicePL.Provider.GetRequiredService<ICourseController>();
        static IMaterialController materialController = ProviderServicePL.Provider.GetRequiredService<IMaterialController>();
        static IPassCourseController passCourseController = ProviderServicePL.Provider.GetRequiredService<IPassCourseController>();

        public static void StartApplication()
        {
            Console.Clear();
            ProgramConsoleMessageHelper.ShowTextForLoginOrRegistration();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.Clear();
                    userController.VerifyLoginAndPassword();
                    break;
                case "2":
                    Console.Clear();
                    userController.CreateNewUser();
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
            Console.Clear();
            ProgramConsoleMessageHelper.ShowTextForFirstStepForAuthorizedUser();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    courseController.CreateNewCourse();
                    break;
                case "2":
                    //passCourseController
                    break;
                case "3":

                    break;
                case "4":

                    break;
                case "5":

                    break;
                case "6":
                    userController.UpdateUser();
                    break;
                case "7":
                    userController.LogOut();
                    Console.Clear();
                    StartApplication();
                    break;
                default:
                    Console.WriteLine("Default case");
                    StartApplication();
                    break;
            }
        }

        public static Material SelectMaterialForAddToCourse()
        {
            Console.Clear();
            MaterialConsoleMessageHelper.ShowTextForChoiceKindOfMaterialForAddToCourse();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    //Video
                    return materialController.CreateVideo();
                case "2":
                    //
                    return materialController.CreateBook();
                case "3":
                    //Article
                    return materialController.CreateArticle();
                case "4":
                    //Article
                    return materialController.GetMaterialFromAllMaterials();
                default:
                    Console.WriteLine("Default case");
                    SelectMaterialForAddToCourse();
                    break;
            }
            return null;
        }

    }
}
