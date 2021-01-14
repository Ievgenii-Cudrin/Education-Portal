using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EducationPortalConsoleApp.InstanceCreator;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Services
{
    public class MaterialService : IMaterialService
    {
        IUnitOfWork uow;
        Material newMaterial;
        public MaterialService()
        {
            this.uow = new EFUnitOfWork();
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
            //MaterialConsoleMessageHelper.ShowObjects(users);

        }

        public bool Delete(int id)
        {
            Material material = uow.Materials.Get(id);
            if (material == null)
            {
                return false;
            }
            else
            {
                uow.Materials.Delete(Convert.ToInt32(material.Id));
            }

            return true;
        }
    }
}
