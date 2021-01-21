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
            $"\n2 Pass the course" +
            $"\n2.See the list of available courses" +
            $"\n3.See the list of passed courses" +
            $"\n4.See the list of courses in progress" +
            $"\n5.Select course to study" +
            $"\n6.Update information about user" +
            $"\n7.LogOut"
            );
        }
    }
}
