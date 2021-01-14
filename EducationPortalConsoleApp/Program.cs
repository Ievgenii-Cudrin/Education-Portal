using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
using EducationPortalConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using EducationPortalConsoleApp.DependencyInjection;
using System;

namespace EducationPortalConsoleApp
{
    class Program
    {
        static void Main()
        {
            ProgramBranch.StartApplication();

            Console.ReadLine();
        }
    }
}
