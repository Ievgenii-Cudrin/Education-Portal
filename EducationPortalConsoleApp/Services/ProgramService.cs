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
            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    CreateMaterial();
                    break;
                case "2":
                    UpdateMaterial();
                    break;
                case "3":
                    ShowAllMaterials();
                    break;
                case "4":
                    DeleteMaterial();
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
    }
}
