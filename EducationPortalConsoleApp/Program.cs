using DataAccessLayer.Entities;
using DataAccessLayer.Serialization;
using EducationPortalConsoleApp.Services;
using System;
using System.Collections.Generic;

namespace EducationPortalConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            ProgramService.StartApp();

            Console.ReadLine();
        }
    }
}
