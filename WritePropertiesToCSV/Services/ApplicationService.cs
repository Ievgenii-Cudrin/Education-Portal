using System;
using System.Collections.Generic;
using System.Reflection;
using WritePropertiesToCSV.Entities;
using WritePropertiesToCSV.FileHelpers;
using System.Linq;
using WritePropertiesToCSV.GetData;

namespace WritePropertiesToCSV.Services
{
    public static class ApplicationService
    {
        public static void StartApp()
        {
            //Create file
            string fileName = "PersonInfo.csv";
            CreateFile.CreateFileCSV(fileName);
            //Get all properties from Person type
            List<PropertyInfo> declaredProperties = typeof(Person).GetTypeInfo().GetProperties().ToList();
            //Get properties from user
            string[] propertiesFromUser = WorkingWithPropertiesService.GetPropertiesFromUser(declaredProperties);
            //Data with filtered properties
            string[] finish = WorkingWithPropertiesService.GetFilteredPersonPersonProperties(PersonList.GetListPerson(), propertiesFromUser, declaredProperties);
            //Add data csv file
            AddDataToCSVFile(finish, fileName);
            Console.WriteLine("Data added successfully");
        }

        public static void AddDataToCSVFile(string[] finish, string fileName)
        {
            //add filtered properties with data to csv file
            foreach (var str in finish)
            {
                WorkingWithFiles.AddRecord(str, fileName);
            }
        }
    }
}
