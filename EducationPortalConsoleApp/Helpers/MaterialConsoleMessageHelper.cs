using DataAccessLayer.Entities;
using EducationPortal.PL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Helpers
{
    public static class MaterialConsoleMessageHelper
    {
        internal static void ShowTextForChoiceCRUDMethod()
        {
            Console.WriteLine($"\nOkey. Make the next choice to continue: " +
            $"\n1.Add material" +
            $"\n2.Update material" +
            $"\n3.Show all materials" +
            $"\n4.Delete material" +
            $"\n5.Return");
        }

        internal static void ShowTextForChoiceKindOfMaterialForAddToCourse()
        {
            Console.WriteLine($"\nPlease, add material to your course: " +
            $"\n1.Add video" +
            $"\n2.Add book" +
            $"\n3.Add article" +
            $"\n4.Add material from exist" +
            $"\n5.Return");
        }

        internal static void MaterialCreated()
        {
            Console.WriteLine("Material successfully created." +
            $"\nSelect next choice" +
            "\n");
        }


            public static void ShowVideoInfo(Material video)
        {
            Video copyOfInputParametrVideo = (Video)video;
            Console.WriteLine($"Id: {copyOfInputParametrVideo.Id}");
            Console.WriteLine($"Name: {copyOfInputParametrVideo.Name}");
            Console.WriteLine($"Link: {copyOfInputParametrVideo.Link}");
            Console.WriteLine($"Video quality: {copyOfInputParametrVideo.Quality}");
            Console.WriteLine("---------------------------");
        }
        public static void ShowArticleInfo(Material article)
        {
            Article copyOfInputParametrArtivle = (Article)article;
            Console.WriteLine($"Id: {copyOfInputParametrArtivle.Id}");
            Console.WriteLine($"Name: {copyOfInputParametrArtivle.Name}");
            Console.WriteLine($"Link: {copyOfInputParametrArtivle.PublicationDate}");
            Console.WriteLine($"Video quality: {copyOfInputParametrArtivle.Site}");
            Console.WriteLine("---------------------------");
        }

        public static void ShowBookInfo(Material book)
        {
            Book copyOfInputParametrBook = (Book)book;
            Console.WriteLine($"Id: {copyOfInputParametrBook.Id}");
            Console.WriteLine($"Book name: {copyOfInputParametrBook.Name}");
            Console.WriteLine($"Book author: {copyOfInputParametrBook.Author}");
            Console.WriteLine($"Count of book pages: {copyOfInputParametrBook.CountOfPages}");
            Console.WriteLine("---------------------------");
        }

        public static void ShowMaterial(List<MaterialViewModel> materialsVM1)
        {
            //ShowMaterials
            foreach (var materialVM in materialsVM1)
            {
                Console.WriteLine($"Id: {materialVM.Id}, {materialVM.ToString()}\n");
            }
        }
    }
}
