using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.InstanceCreator;
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
            MaterialConsoleMessageHelper.ShowTextForChoiceCRUDMethod();

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

        void CreateMaterial()
        {
            MaterialConsoleMessageHelper.ShowTextForChoiceKindOfMaterial();
            string kindOfMaterial = Console.ReadLine();
            Material material;
            switch (kindOfMaterial)
            {
                case "1":
                    material = VideoInstanceCreator.VideoCreator();
                    _uow.Materials.Create(material);
                    break;
                case "2":
                    material = BookInstanceCreator.BookCreator();
                    _uow.Materials.Create(material);
                    break;
                case "3":
                    material = ArticleInstanceCreator.ArticleCreator();
                    _uow.Materials.Create(material);
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

        }

        void UpdateMaterial()
        {

        }

        void ShowAllMaterials()
        {

        }

        void DeleteMaterial()
        {

        }
    }
}
