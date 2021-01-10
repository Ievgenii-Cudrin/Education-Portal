using EducationPortalConsoleApp.Controller;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Branch
{
    public class ProgrammBranch
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
    }
}
