using LINQ.Entity;
using LINQ.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {

        static void Main(string[] args)
        {

            List<Customer> customers = GetData.GetCustomer();
            //1
            Customer c1 = customers.OrderBy(x => x.RegistrationDate).FirstOrDefault();

            //2
            double srednee = customers.Average(x => x.Balance);

            //3
            Console.WriteLine("Enter first date: ");
            DateTime firstDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter second date: ");
            DateTime lastDate = DateTime.Parse(Console.ReadLine());


            IEnumerable<Customer> sortCustomerByDates = customers.Where(x => x.RegistrationDate.Date > firstDate && x.RegistrationDate < lastDate).OrderBy(x => x.RegistrationDate);
            if (sortCustomerByDates == null)
            {
                Console.WriteLine("No results");
            }
            else
            {
                foreach (var customer in sortCustomerByDates)
                {
                    Console.WriteLine($"{customer.Name} - {customer.RegistrationDate}");
                }
            }


            //4
            long id = Convert.ToInt32(Console.ReadLine());
            var c2 = customers.Where(x => x.Id.ToString().Contains(id.ToString()));

            //5
            string name = Console.ReadLine();
            var c3 = customers.Where(x => x.Name.ToLower().Contains(name.ToLower()));

            //6
            var groupUserByMonth = customers.GroupBy(x => x.RegistrationDate.Month).Select(x => x.OrderBy(k => k.Name));


            //7
            Type t = typeof(Customer);
            var allProperties = t.GetProperties();
            Console.WriteLine("Введите свойство! Доступные свойства: ");
            foreach (var prop in allProperties)
            {
                Console.Write(prop.Name + ",");
            }
            string propertyFromUser = Console.ReadLine();

            Console.WriteLine("Ascending or descending (write A or D): ");
            string sorting = Console.ReadLine();

            IEnumerable<Customer> sortByUserChoice;

            if (sorting.ToLower() == "a")
            {
                switch (propertyFromUser.ToLower())
                {
                    case "id":
                        sortByUserChoice = customers.OrderBy(x => x.Id);
                        break;
                    case "name":
                        sortByUserChoice = customers.OrderBy(x => x.Name);
                        break;
                    case "RegistrationDate":
                        sortByUserChoice = customers.OrderBy(x => x.RegistrationDate);
                        break;
                    case "Balance":
                        sortByUserChoice = customers.OrderBy(x => x.Balance);
                        break;
                    default:
                        break;

                }
            }
            else if (sorting.ToLower() == "d")
            {
                switch (propertyFromUser.ToLower())
                {
                    case "id":
                        sortByUserChoice = customers.OrderByDescending(x => x.Id);
                        break;
                    case "name":
                        sortByUserChoice = customers.OrderByDescending(x => x.Name);
                        break;
                    case "RegistrationDate":
                        sortByUserChoice = customers.OrderByDescending(x => x.RegistrationDate);
                        break;
                    case "Balance":
                        sortByUserChoice = customers.OrderByDescending(x => x.Balance);
                        break;
                    default:
                        break;

                }
            }




            //8

            string str = string.Join(",", customers.Select(customer => customer.Name));
            Console.WriteLine(str);
            foreach (var customer in customers)
            {
                Console.Write($"{customer.Name},");
            }


            Console.ReadLine();
        }
    }
}
