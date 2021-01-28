using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.Helpers
{
    public static class PassCourseConsoleMessageHelper
    {
        public static void ShowInfoToStartPassingCourse()
        {
            Console.WriteLine("Let's start studying the materials." +
                    "\n After reading, put + in front of the material." +
                    "\n Any other value will not count the passage of the material \n");
        }
    }
}
