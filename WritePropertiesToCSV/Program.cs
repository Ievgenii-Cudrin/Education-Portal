using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WritePropertiesToCSV.Entities;
using System.Linq;
using WritePropertiesToCSV.GetData;

namespace WritePropertiesToCSV
{
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
            //add filtered properties with data to csv file
            foreach (var str in finish)
            {
                AddRecord(str, fileName);
            }
        }

        private static string[] GetFilteredPersonPersonProperties(List<Person> getListPerson, string[] propertiesFromUser, List<PropertyInfo> declaredProperties)
        {
            string[] allFilteredPropertiesWithValueForAllUser = new string[getListPerson.Count];

            for (int i = 0; i < getListPerson.Count; i++)
            {
                string filterUserProperties = "";

                for (int j = 0; j < propertiesFromUser.Length; j++)
                {
                    filterUserProperties = WritePropertiesForOneUser(getListPerson[i], propertiesFromUser[j], declaredProperties);
                }

                allFilteredPropertiesWithValueForAllUser[i] = filterUserProperties;
            }

            return allFilteredPropertiesWithValueForAllUser;
        }

        public static string WritePropertiesForOneUser(Person person, string propertyFromUser, List<PropertyInfo> declaredProperties)
        {
            //create finall string with all filtered properties
            string stringWithFilterProperties = "";

            for (int k = 0; k < declaredProperties.Count; k++)
            {
                if (propertyFromUser.Replace(" ", "").ToLower() == declaredProperties[k].Name.ToLower())
                {
                    //Check value in property
                    PropertyInfo property = person.GetType().GetProperty(declaredProperties[k].Name, BindingFlags.Instance | BindingFlags.Public);

                    //Get value from this property if there is no data, assign null
                    string valueInProperty = property.GetValue(person, null) != null ? property.GetValue(person, null).ToString() : null;

                    //If value exist, add to to string with all filtered properti
                    stringWithFilterProperties = stringWithFilterProperties + GetString(property, person, valueInProperty);
                }
            }

            return stringWithFilterProperties;
        }

        static string GetString(PropertyInfo property, Person person, string valueInProperty)
        {
            //create new string
            string stringWithFilteredPropertyForUser = "";

            //check string - is null or == 0
            if (valueInProperty != null && valueInProperty != "0")
            {
                //Add property with value to string
                stringWithFilteredPropertyForUser = stringWithFilteredPropertyForUser + $"{property.Name}: " + typeof(Person).GetProperty(property.Name).GetValue(person).ToString() + ",";
            }

            return stringWithFilteredPropertyForUser;
        }

        private static string[] GetPropertiesFromUser(IEnumerable<PropertyInfo> typeProperties)
        {
            //Show user available properties
            Console.WriteLine("You can write only this properties: ");
            
            foreach (var property in typeProperties)
            {
                Console.WriteLine($"{property.Name}");
            }

            Console.Write("\nPlease, enter user properties: ");

            //take properties from user
            string[] massOfpropertiesFromUser = Console.ReadLine().Split(',');

            return massOfpropertiesFromUser;
        }

        public static void AddRecord(string data, string path)
        {
            try
            {
                //write to csv
                using (StreamWriter file = new StreamWriter(path, true))
                {
                    file.WriteLine(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateFile(string path)
        {
            //create file
            var file = new FileInfo(path);

            FileStream stream;

            if (!file.Exists)
            {
                stream = file.Create();
            }
            else
            {
                return;
            }

            stream.Close();
        }
    }
}
