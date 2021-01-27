using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Helpers
{
    public static class ProgramConsoleMessageHelper
    {
        internal static void ShowFunctionResult(bool success, string messageSuccess, string messageForWrong, Action forSuccess, Action forWrong)
        {
            if (success)
            {
                Console.WriteLine(messageSuccess);
                forSuccess();
            }
            else
            {
                Console.WriteLine(messageForWrong);
                forWrong();
            }
        }

        internal static void ShowTextForLoginOrRegistration()
        {
            Console.WriteLine($"Hi, dear user. Please, make your choice: " +
            $"\n1.Login" +
            $"\n2.Registration"
            );
        }

        internal static void ShowTextForFirstStepForAuthorizedUser()
        {
            Console.WriteLine($"Please, make your choice: " +
            $"\n1.Create course" +
            $"\n2 Pass the available course" +
            $"\n3 Сontinue the started course" +
            $"\n4.See the list of all passed courses" +
            $"\n5.See the list of courses in progress" +
            $"\n6.My information" +
            $"\n7.LogOut"
            );
        }
    }
}
