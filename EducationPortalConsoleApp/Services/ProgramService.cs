using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Services
{
    public static class ProgramService
    {
        public static void SelectEntityToWork()
        {
            //TODO Console - 
            ProgramServiceConsoleMessageHelper.ShowTextWithEntityToSelect();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    new UserService().StartWorkWithUser();
                    break;
                case "2":
                    new MaterialService().StartWorkWithMaterial();
                    break;
                //case "3":
                //    //ShowAllMaterials();
                //    break;
                //case "4":
                //    //DeleteMaterial();
                //    break;
                default:
                    Console.WriteLine("Default case");
                    SelectEntityToWork();
                    break;
            }
        }
    }
}
