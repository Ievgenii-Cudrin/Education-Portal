using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Helpers
{
    public static class MaterialConsoleMessageHelper
    {
        public static void ShowTextForChoiceCRUDMethod()
        {
            Console.WriteLine($"Okey. Make the next choice to continue: " +
            $"\n1.Add material" +
            $"\n2.Update material" +
            $"\n3.Show all materials" +
            $"\n4.Delete material" +
            $"\n5.Return");
        }

        public static void ShowTextForChoiceKindOfMaterial()
        {
            Console.WriteLine($"Make the next choice to continue: " +
            $"\n1.Add video" +
            $"\n2.Add book" +
            $"\n3.Add article" +
            $"\n4.Return");
        }

        public static void MaterialCreated()
        {
            Console.WriteLine("Material successfully created." +
            $"\nSelect next choice" +
            "\n");
        }


        public static void ShowVideoInfo(Material video) { }
        public static void ShowArticleInfo(Material article) { }
        public static void ShowBookInfo(Material book) { }
    }
}
