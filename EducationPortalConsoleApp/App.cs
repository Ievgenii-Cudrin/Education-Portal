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
                    await this.userController.VerifyLoginAndPassword();
                    break;
                case "2":
                    Console.Clear();
                    await this.userController.CreateNewUser();
                    await this.StartApplication();
                    break;
                default:
                    Console.WriteLine("Default case");
                    await this.StartApplication();
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
                    await this.courseController.CreateNewCourse();
                    break;
                case "2":
                    await this.passCourseController.StartPassCourse();
                    break;
                case "3":
                    await this.passCourseController.StartPassingCourseFromProgressList();
                    break;
                case "4":
                    await this.userController.ShowAllPassedCourses();
                    break;
                case "5":
                    await this.userController.ShowAllUserSkills();
                    break;
                case "6":
                    await this.userController.ShowAllCourseInProggres();
                    break;
                case "7":
                    this.userController.ShowUserInfo();
                    break;
                case "8":
                    this.userController.LogOut();
                    Console.Clear();
                    await this.StartApplication();
                    break;
                default:
                    Console.WriteLine("Default case");
                    await this.StartApplication();
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
                    return await this.materialController.CreateVideo();
                case "2":
                    // Book
                    return await this.materialController.CreateBook();
                case "3":
                    // Article
                    return await this.materialController.CreateArticle();
                case "4":
                    return await this.materialController.GetMaterialFromAllMaterials(courseId);
                default:
                    Console.WriteLine("Default case");
                    await this.SelectMaterialForAddToCourse(courseId);
                    break;
            }

            return null;
        }
    }
}
