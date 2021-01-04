using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Helpers
{
    public static class ProgramServiceConsoleMessageHelper
    {
        public static void ShowTextWithEntityToSelect()
        {
            Console.WriteLine($"Hi, dear user. Please, make your choice: " +
            $"\n1.Operation with users" +
            $"\n2.Operation with materials"
            //$"\n3.Operation with courses" +
            //$"\n4.Operation with skills"
            );
        }
    }
}
