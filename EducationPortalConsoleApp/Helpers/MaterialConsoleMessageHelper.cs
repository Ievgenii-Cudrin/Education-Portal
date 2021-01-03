using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Helpers
{
    public static class MaterialConsoleMessageHelper
    {
        public static void ShowTextForChoice()
        {
            Console.WriteLine($"Hi, dear user. Please, make your choice: " +
            $"\n1.Create material" +
            $"\n2.Update material" +
            $"\n3.Show all materials" +
            $"\n4.Delete material");
        }
    }
}
