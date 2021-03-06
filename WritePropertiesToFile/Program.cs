﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace WritePropertiesToFile
{
    public class Person
    {
        public int Age { get; set; }
        public string EyeColor { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public decimal? Salary { get; set; }
    }
    public static class PersonList
    {
        public static List<Person> GetListPerson()
        {
            return new List<Person>
            {
                new Person
                {
                    Age = 24,
                    EyeColor = "blue",
                    Name = "Juarez Mayo",
                    Gender = "male",
                    Company = "MAGNEATO",
                    Address= "284 Kansas Place, Beyerville, Pennsylvania, 5206",
                    Salary = (decimal?) 345.6
                },
                new Person
                {
                    Age= 26,
                    EyeColor= "green",
                    Name= "Orr Love",
                    Gender= "male",
                    Company= "BULLZONE",
                    Address= "893 Beaver Street, Johnsonburg, Nebraska, 503",
                    Salary = (decimal?) 99.32
                },
                new Person
                {
                    Age= 32,
                    EyeColor= "blue",
                    Name= "Mccall Munoz",
                    Gender= "male",
                    Company= "DOGNOST",
                    Address= "850 Mill Road, Chemung, Mississippi, 2962",
                    Salary = (decimal?) 3000.89
                },
                new Person
                {
                    EyeColor= "green",
                    Name= "Strong Downs",
                    Gender= "male",
                    Company= "BEADZZA",
                    Address= "377 Homecrest Court, Tuskahoma, New Jersey, 3583"
                },
                new Person
                {
                    EyeColor= "brown",
                    Name = "Sarah Pope",
                    Gender = "female"
                }
            };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StartApp();
            Console.ReadLine();
        }

        static void StartApp()
        {
            //Create file
            string fileName = "PersonInfo.csv";
            CreateFile(fileName);

            //Get all properties from Person type
            List<PropertyInfo> declaredProperties = typeof(Person).GetTypeInfo().GetProperties().ToList();
            string[] propertiesFromUser = GetPropertiesFromUser(declaredProperties);

            //Data with filtered properties
            string[] finish = GetFilteredPersonPersonProperties(PersonList.GetListPerson(), propertiesFromUser, declaredProperties);

            //Add data csv file
            AddDataToCSVFile(finish, fileName);

            Console.WriteLine("Data added successfully");
        }

        private static void AddDataToCSVFile(string[] finish, string fileName)
        {
            foreach(var str in finish)
            {
                AddRecord(str, fileName);
            }
        }

        private static string[] GetFilteredPersonPersonProperties(List<Person> getListPerson, string[] propertiesFromUser, List<PropertyInfo> declaredProperties)
        {
            string[] finish = new string[getListPerson.Count];
            for(int i = 0; i < getListPerson.Count; i++)
            {
                string filterUserProperties = "";
                for(int j = 0; j < propertiesFromUser.Length; j++)
                {
                    for(int k = 0; k< declaredProperties.Count; k++)
                    {
                        if (propertiesFromUser[j].Replace(" ", "").ToLower() == declaredProperties[k].Name.ToLower())
                        {
                            //Check value in property
                            PropertyInfo prop = getListPerson[i].GetType().GetProperty(declaredProperties[k].Name, BindingFlags.Instance | BindingFlags.Public);
                            var val = prop.GetValue(getListPerson[i], null) != null ? prop.GetValue(getListPerson[i], null).ToString() : null;

                            if (val != null && val != "0")
                            {
                                //Add property with value to string
                                filterUserProperties = filterUserProperties + $"{declaredProperties[k].Name}: " + typeof(Person).GetProperty(declaredProperties[k].Name).GetValue(getListPerson[i]).ToString() + ",";
                                break;
                            }
                        }
                    }
                    
                }
                finish[i] = filterUserProperties;
            }
            return finish;
        }


        private static string[] GetPropertiesFromUser(IEnumerable<PropertyInfo> typeProperties)
        {
            Console.WriteLine("You can write only this properties: ");
            foreach(var property in typeProperties)
            {
                Console.WriteLine($"{property.Name}");
            }
            Console.Write("\nPlease, enter user properties: ");
            string[] massOfpropertiesFromUser = Console.ReadLine().Split(',');
            return massOfpropertiesFromUser; 
        }

        public static void  AddRecord(string data, string path)
        {
            try
            {
                using(StreamWriter file = new StreamWriter(path, true))
                {
                    file.WriteLine(data);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateFile(string path)
        {
            var file = new FileInfo(path);
            FileStream stream;
            if (!file.Exists)
                stream = file.Create();
            else
                return;

            stream.Close();
        }
    }
}
