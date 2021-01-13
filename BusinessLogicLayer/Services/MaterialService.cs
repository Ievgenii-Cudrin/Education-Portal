using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EducationPortalConsoleApp.InstanceCreator;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Services
{
    public class MaterialService
    {
        IUnitOfWork uow;
        Material newMaterial;
        public MaterialService()
        {
            this.uow = new EFUnitOfWork();
        }

        public void StartWorkWithMaterial()
        {
            //MaterialConsoleMessageHelper.ShowTextForChoiceCRUDMethod();

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
            //MaterialConsoleMessageHelper.ShowTextForChoiceKindOfMaterial();
            string kindOfMaterial = Console.ReadLine();
            Material material;
            switch (kindOfMaterial)
            {
                case "1":
                    material = VideoInstanceCreator.VideoCreator();
                    uow.Materials.Create(material);
                    ContinueAfterMaterialCreated();
                    break;
                case "2":
                    material = BookInstanceCreator.BookCreator();
                    uow.Materials.Create(material);
                    ContinueAfterMaterialCreated();
                    break;
                case "3":
                    material = ArticleInstanceCreator.ArticleCreator();
                    uow.Materials.Create(material);
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

        //TODO ----

        //public bool CreateVideo(string name, int quality, int duration, string link)
        //{
        //    bool success = true;
        //    if (name.Length > 0 && quality > 0 && duration > 1 && link != null)
        //    {
        //        newMaterial = new Video()
        //        {
        //            Name = name,
        //            Quality = quality,
        //            Duration = duration,
        //            Link = link
        //        };
        //        uow.Materials.
        //    }
        //}

        void UpdateMaterial()
        {
            //TODO finish this method
            Console.Write($"Enter material ID to update: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Material material = uow.Materials.Get(id);
            if (material == null)
            {
                Console.WriteLine($"Material not found");
                StartWorkWithMaterial();
            }
            else
            {
                //TODO (Lisskov principe)
                if (material is Video)
                    material = VideoInstanceCreator.VideoCreator();
                else if (material is Article)
                    material = ArticleInstanceCreator.ArticleCreator();
                else
                    material = BookInstanceCreator.BookCreator();

                material.Id = id;
                uow.Materials.Update(material);
                Console.WriteLine("Material has been successfully updated");
                StartWorkWithMaterial();
            }
        }

        void ShowAllMaterials()
        {
            IEnumerable<Material> materials = uow.Materials.GetAll();
            foreach(var material in materials)
            {
                //if (material is Video)
                    //MaterialConsoleMessageHelper.ShowVideoInfo(material);
                //else if (material is Article)
                    //MaterialConsoleMessageHelper.ShowArticleInfo(material);
                //else
                    //MaterialConsoleMessageHelper.ShowBookInfo(material);
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

            Material material = uow.Materials.Get(id);
            if (material == null)
                Console.WriteLine($"\nMaterial not found\n");
            else
                uow.Materials.Delete(Convert.ToInt32(material.Id));
    }
}
