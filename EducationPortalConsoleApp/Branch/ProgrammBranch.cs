﻿using EducationPortalConsoleApp.Controller;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Branch
{
    public static class ProgrammBranch
    {
        public static void StartApplication()
        {
            ProgramConsoleMessageHelper.ShowTextForLoginOrRegistration();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    new UserController().VerifyLoginAndPassword();
                    break;
                case "2":
                    new UserController().CreateNewUser();
                    break;
                //case "3":
                //    //ShowAllMaterials();
                //    break;
                //case "4":
                //    //DeleteMaterial();
                //    break;
                default:
                    Console.WriteLine("Default case");
                    //SelectEntityToWork();
                    break;
            }
        }
    }
}
