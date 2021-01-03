using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Services
{
    public class MaterialService
    {
        IUnitOfWork _uow;
        public MaterialService()
        {
            this._uow = new EFUnitOfWork();
        }
        public void StartApp()
        {
            MaterialConsoleMessageHelper.ShowTextForChoice();

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

            void CreateMaterial()
            {
                _uow.Users.Create(UserGetDataHelper.GenerateObjectFromUser());
            }
        }
    }
}
