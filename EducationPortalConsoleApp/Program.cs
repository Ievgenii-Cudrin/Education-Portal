using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Services;
using System;

namespace EducationPortalConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new ProgrammBranch().StartApplication();

            Console.ReadLine();
        }
    }
}
