using LINQ.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;
using System.Reflection;

namespace LINQ.Helpers
{
    public static class WorkWithData
    {
        static List<Customer> customers = GetData.GetCustomer();

        public static Customer GetFirstRegistratedCistomer()
        {
            return customers.OrderBy(x => x.RegistrationDate).FirstOrDefault();
        }

        public static double GetAverageByBalanceAllCustomer()
        {
            return customers.Average(x => x.Balance);
        }

        public static IEnumerable<Customer> GetFilteredByDateCustomers()
        {
            //Get first and last dates
            Console.WriteLine("Enter first date (e.g. 20.10.1987): ");
            DateTime firstDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter last date (e.g. 20.10.1987): ");
            DateTime lastDate = DateTime.Parse(Console.ReadLine());
            //get filtered by dates customer
            IEnumerable<Customer> filteredByDate = customers.Where(x => x.RegistrationDate > firstDate && x.RegistrationDate < lastDate);

            if(filteredByDate == null)
            {
                Console.WriteLine("No results");
                return null;
            }
            else
            {
                return filteredByDate;
            }
        }

        public static IEnumerable<Customer> GetCustomerById()
        {
            Console.WriteLine("Enter id: ");
            string id = Console.ReadLine();

            return customers.Where(x => x.Id.ToString().Contains(id));
        }

        public static IEnumerable<Customer> GetFilterByPartOfNameCustomer()
        {
            Console.WriteLine("Enter customer name: ");
            string partOfName = Console.ReadLine();

            return customers.Where(x => x.Name.ToLower().Contains(partOfName.ToLower()));
        }

        public static void GroupByMonth()
        {
            var groups = customers.OrderBy(x => x.RegistrationDate.Month).GroupBy(x => x.RegistrationDate.Month).Select(x => x.OrderBy(k => k.Name));
            foreach(var group in groups)
            {
                foreach(var customer in group)
                {
                    Console.WriteLine(customer.Name);
                    Console.WriteLine(customer.RegistrationDate);
                    Console.WriteLine("-");
                }
                Console.WriteLine("--------------");
            }
        }

        public static void ShowAllNames()
        {
            string names = string.Join(",", customers.Select(customer => customer.Name));
            Console.WriteLine(names);
        }


        public static IEnumerable<Customer> ShowCustomerBySomeProperty()
        {
            Console.WriteLine("Enter property! Available properties: ");

            //Show available properties
            foreach (var property in typeof(Customer).GetProperties())
            {
                Console.Write(property.Name + ",");
            }

            Console.WriteLine("");
            //get property from user
            string propertyFromUser = Console.ReadLine();
            PropertyInfo prop = typeof(Customer).GetProperty(propertyFromUser);
            //get sorting type
            Console.WriteLine("Ascending or descending (write A or D): ");
            string sorting = Console.ReadLine();

            if (sorting.ToLower() == "a")
            {
                return customers.OrderBy(x => prop.GetValue(x, null));
            }
            else if (sorting.ToLower() == "d")
            {
                return customers.OrderByDescending(x => prop.GetValue(x, null));
            }
            return null;
        }
    }
}
