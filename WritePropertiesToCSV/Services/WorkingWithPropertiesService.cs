using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WritePropertiesToCSV.Entities;

namespace WritePropertiesToCSV.Services
{
    public static class WorkingWithPropertiesService
    {
        public static string[] GetFilteredPersonPersonProperties(List<Person> getListPerson, string[] propertiesFromUser, List<PropertyInfo> declaredProperties)
        {
            string[] allFilteredPropertiesWithValueForAllUser = new string[getListPerson.Count];

            for (int i = 0; i < getListPerson.Count; i++)
            {
                //Create string wiht filtered properties for one user
                var filterUserProperties = "";

                for (int j = 0; j < propertiesFromUser.Length; j++)
                {
                    //set all filtered properties with value for one user
                    filterUserProperties = filterUserProperties + WriteToStringFilteredPropertiesForOneUser(getListPerson[i], propertiesFromUser[j], declaredProperties);
                }

                //add string with filtered properties for one user to array with strings
                allFilteredPropertiesWithValueForAllUser[i] = filterUserProperties;
            }

            return allFilteredPropertiesWithValueForAllUser;
        }

        public static string WriteToStringFilteredPropertiesForOneUser(Person person, string propertyFromUser, List<PropertyInfo> declaredProperties)
        {
            //create finall string with all filtered properties
            var stringWithFilterProperties = "";

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

        public static string GetString(PropertyInfo property, Person person, string valueInProperty)
        {
            //create new string
            var stringWithFilteredPropertyForUser = "";

            //check string - is null or == 0
            if (valueInProperty != null && valueInProperty != "0")
            {
                //Add property with value to string
                stringWithFilteredPropertyForUser = stringWithFilteredPropertyForUser + $"{property.Name}: " + typeof(Person).GetProperty(property.Name).GetValue(person).ToString() + ",";
            }

            return stringWithFilteredPropertyForUser;
        }

        public static string[] GetPropertiesFromUser(IEnumerable<PropertyInfo> typeProperties)
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
    }
}
