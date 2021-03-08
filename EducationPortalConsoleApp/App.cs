using System;
using System.Threading.Tasks;
using EducationPortal.PL.Interfaces;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
using Entities;

namespace EducationPortal.PL
{
    public class App : IApplication
    {
        private readonly IUserController userController;
        private readonly ICourseController courseController;
        private readonly IMaterialController materialController;
        private readonly IPassCourseController passCourseController;

        public App(
            IUserController userController,
            ICourseController courseController,
            IMaterialController materialController,
            IPassCourseController passCourseController)
        {
            this.userController = userController;
            this.userController.WithApplication(this);
            this.courseController = courseController;
            this.courseController.WithApplication(this);
            this.materialController = materialController;
            this.passCourseController = passCourseController;
            this.passCourseController.WithApplication(this);
        }

        public async Task StartApplication()
        {
            Console.Clear();
            ProgramConsoleMessageHelper.ShowTextForLoginOrRegistration();
            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.Clear();
                    await userController.VerifyLoginAndPassword();
                    break;
                case "2":
                    Console.Clear();
                    await userController.CreateNewUser();
                    StartApplication();
                    break;
                default:
                    Console.WriteLine("Default case");
                    await StartApplication();
                    break;
            }
        }

        public async Task SelectFirstStepForAuthorizedUser()
        {
            Console.Clear();
            ProgramConsoleMessageHelper.ShowTextForFirstStepForAuthorizedUser();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    await courseController.CreateNewCourse();
                    break;
                case "2":
                    await passCourseController.StartPassCourse();
                    break;
                case "3":
                    await passCourseController.StartPassingCourseFromProgressList();
                    break;
                case "4":
                    await userController.ShowAllPassedCourses();
                    break;
                case "5":
                    await userController.ShowAllUserSkills();
                    break;
                case "6":
                    await userController.ShowAllCourseInProggres();
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

        public async Task<Material> SelectMaterialForAddToCourse(int courseId)
        {
            Console.Clear();
            MaterialConsoleMessageHelper.ShowTextForChoiceKindOfMaterialForAddToCourse();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    // Video
                    return await materialController.CreateVideo();
                case "2":
                    // Book
                    return await materialController.CreateBook();
                case "3":
                    // Article
                    return await materialController.CreateArticle();
                case "4":
                    return await materialController.GetMaterialFromAllMaterials(courseId);
                default:
                    Console.WriteLine("Default case");
                    SelectMaterialForAddToCourse(courseId);
                    break;
            }

            return null;
        }
    }
}
