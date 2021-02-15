using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WritePropertiesToCSV.Entities;
using System.Linq;
using WritePropertiesToCSV.GetData;
using WritePropertiesToCSV.FileHelpers;
using WritePropertiesToCSV.Services;

namespace WritePropertiesToCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationService.StartApp();
            Console.ReadLine();
        }
    }
}
