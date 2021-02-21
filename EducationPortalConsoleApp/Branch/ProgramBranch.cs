namespace EducationPortalConsoleApp.Branch
{
    using System;
    using EducationPortal.PL.Interfaces;
    using EducationPortalConsoleApp.Helpers;
    using EducationPortalConsoleApp.Interfaces;
    using Entities;
    using Microsoft.Extensions.DependencyInjection;

    public static class ProgramBranch
    {
        private static IUserController userController = ProviderServicePL.Provider.GetRequiredService<IUserController>();
        private static ICourseController courseController = ProviderServicePL.Provider.GetRequiredService<ICourseController>();
        private static IMaterialController materialController = ProviderServicePL.Provider.GetRequiredService<IMaterialController>();
        private static IPassCourseController passCourseController = ProviderServicePL.Provider.GetRequiredService<IPassCourseController>();

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
                    passCourseController.StartPassCourse();
                    break;
                case "3":
                    passCourseController.StartPassingCourseFromProgressList();
                    break;
                case "4":
                    userController.ShowAllPassedCourses();
                    break;
                case "5":
                    userController.ShowAllUserSkills();
                    break;
                case "6":
                    userController.ShowAllCourseInProggres();
                    break;
                case "7":
                    userController.ShowUserInfo();
                    break;
                case "8":
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

        public static Material SelectMaterialForAddToCourse(int courseId)
        {
            Console.Clear();
            MaterialConsoleMessageHelper.ShowTextForChoiceKindOfMaterialForAddToCourse();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    // Video
                    return materialController.CreateVideo();
                case "2":
                    // Book
                    return materialController.CreateBook();
                case "3":
                    // Article
                    return materialController.CreateArticle();
                case "4":
                    return materialController.GetMaterialFromAllMaterials(courseId);
                default:
                    Console.WriteLine("Default case");
                    SelectMaterialForAddToCourse(courseId);
                    break;
            }

            return null;
        }
    }
}
