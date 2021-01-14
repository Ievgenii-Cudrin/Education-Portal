using DataAccessLayer.DependencyInjection;
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
            var provider = Startup.ConfigureService();
            new IProgramBranch().StartApplication();

            Console.ReadLine();
        }
    }
}
