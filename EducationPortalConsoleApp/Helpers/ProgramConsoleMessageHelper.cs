using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Helpers
{
    public static class ProgramConsoleMessageHelper
    {
        internal static void ShowFunctionResult(bool success, string message)
        {
            if (success)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Somthing wrong");
            }
        }

        internal static void ShowTextForLoginOrRegistration()
        {
            Console.WriteLine($"Hi, dear user. Please, make your choice: " +
            $"\n1.Login" +
            $"\n2.Registration" +
            $"\n3.Show all users"
            );
        }

        //internal static void ShowTextWithEntityToSelect()
        //{
        //    Console.WriteLine($"Hi, dear user. Please, make your choice: " +
        //    $"\n1.Operation with users" +
        //    $"\n2.Operation with materials"
        //    //$"\n3.Operation with courses" +
        //    //$"\n4.Operation with skills"
        //    );
        //}

        internal static void ShowTextForFirstStepForAuthorizedUser()
        {
            Console.WriteLine($"Please, make your choice: " +
            $"\n1.Create course" +
            $"\n2.See the list of available courses" +
            $"\n3.See the list of passed courses" +
            $"\n4.See the list of courses in progress" +
            $"\n5.Select course to study" +
            $"\n6.LogOut"
            );
        }
    }
}
