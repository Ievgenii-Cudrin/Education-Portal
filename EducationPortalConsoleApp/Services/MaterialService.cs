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

        public void StartWorkWithMaterial()
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
                case "5":
                    ProgramService.SelectEntityToWork();
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
                    ContinueAfterMaterialCreated();
                    break;
                case "2":
                    material = BookInstanceCreator.BookCreator();
                    _uow.Materials.Create(material);
                    ContinueAfterMaterialCreated();
                    break;
                case "3":
                    material = ArticleInstanceCreator.ArticleCreator();
                    _uow.Materials.Create(material);
                    ContinueAfterMaterialCreated();
                    break;
                case "4":
                    StartWorkWithMaterial();
                    break;
                default:
                    Console.WriteLine("Default case. Please, try again!");
                    CreateMaterial();
                    break;
            }
        }

        void UpdateMaterial()
        {
            //TODO finish this method
            Console.Write($"Enter material ID to update: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Material material = _uow.Materials.Get(id);
            if (material == null)
            {
                Console.WriteLine($"Material not found");
                StartWorkWithMaterial();
            }
            else
            {
                //TODO
                if (material is Video)
                    material = VideoInstanceCreator.VideoCreator();
                else if (material is Article)
                    material = ArticleInstanceCreator.ArticleCreator();
                else
                    material = BookInstanceCreator.BookCreator();

                material.Id = id;
                _uow.Materials.Update(material);
                Console.WriteLine("Material has been successfully updated");
                StartWorkWithMaterial();
            }
        }

        void ShowAllMaterials()
        {
            IEnumerable<Material> materials = _uow.Materials.GetAll();
            foreach(var material in materials)
            {
                if (material is Video)
                    MaterialConsoleMessageHelper.ShowVideoInfo(material);
                else if (material is Article)
                    MaterialConsoleMessageHelper.ShowArticleInfo(material);
                else
                    MaterialConsoleMessageHelper.ShowBookInfo(material);
            }
            Console.WriteLine("\n");
            StartWorkWithMaterial();
            //MaterialConsoleMessageHelper.ShowObjects(users);

            StartWorkWithMaterial();
        }

        void DeleteMaterial()
        {
            Console.Write($"Enter material ID to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Material material = _uow.Materials.Get(id);
            if (material == null)
                Console.WriteLine($"\nMaterial not found\n");
            else
                _uow.Materials.Delete(Convert.ToInt32(material.Id));

            StartWorkWithMaterial();
        }

        void ContinueAfterMaterialCreated()
        {
            MaterialConsoleMessageHelper.MaterialCreated();
            StartWorkWithMaterial();
        }
    }
}
