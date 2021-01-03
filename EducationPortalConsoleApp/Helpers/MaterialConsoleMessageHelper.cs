using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Helpers
{
    public static class MaterialConsoleMessageHelper
    {
        public static void ShowTextForChoiceCRUDMethod()
        {
            Console.WriteLine($"Hi, dear user. Please, make your choice: " +
            $"\n1.Add material" +
            $"\n2.Update material" +
            $"\n3.Show all materials" +
            $"\n4.Delete material");
        }

        public static void ShowTextForChoiceKindOfMaterial()
        {
            Console.WriteLine($"Hi, dear user. Please, make your choice: " +
            $"\n1.Add video" +
            $"\n2.Add book" +
            $"\n3.Add article");
        }
    }
}
