using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {

        static void Main(string[] args)
        {

            List<Customer> customers = GetCustomer();
            //1
            Customer c1 = GetCustomer().OrderBy(x => x.RegistrationDate).FirstOrDefault();

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

            foreach (var customer in customers)
            {
                Console.Write($"{customer.Name},");
            }


            Console.ReadLine();
        }


        static List<Customer> GetCustomer()
        {
            var customers = new List<Customer>
            {
                 new Customer(1, "Tawana Shope", new DateTime(2017, 7, 15), 15.8),
                 new Customer(2, "Danny Wemple", new DateTime(2016, 2, 3), 88.54),
                 new Customer(3, "Margert Journey", new DateTime(2017, 11, 19), 0.5),
                 new Customer(4, "Tyler Gonzalez", new DateTime(2017, 5, 14), 184.65),
                 new Customer(5, "Melissa Demaio", new DateTime(2016, 9, 24), 241.33),
                 new Customer(6, "Cornelius Clemens", new DateTime(2016, 4, 2), 99.4),
                 new Customer(7, "Silvia Stefano", new DateTime(2017, 7, 15), 40),
                 new Customer(8, "Margrett Yocum", new DateTime(2017, 12, 7), 62.2),
                 new Customer(9, "Clifford Schauer", new DateTime(2017, 6, 29), 89.47),
                 new Customer(10, "Norris Ringdahl", new DateTime(2017, 1, 30), 13.22),
                 new Customer(11, "Delora Brownfield", new DateTime(2011, 10, 11), 0),
                 new Customer(12, "Sparkle Vanzile", new DateTime(2017, 7, 15), 12.76),
                 new Customer(13, "Lucina Engh", new DateTime(2017, 3, 8), 19.7),
                 new Customer(14, "Myrna Suther", new DateTime(2017, 8, 31), 13.9),
                 new Customer(15, "Fidel Querry", new DateTime(2016, 5, 17), 77.88),
                 new Customer(16, "Adelle Elfrink", new DateTime(2017, 11, 6), 183.16),
                 new Customer(17, "Valentine Liverman", new DateTime(2017, 1, 18), 13.6),
                 new Customer(18, "Ivory Castile", new DateTime(2016, 4, 21), 36.8),
                 new Customer(19, "Florencio Messenger", new DateTime(2017, 10, 2), 36.8),
                 new Customer(20, "Anna Ledesma", new DateTime(2017, 12, 29), 0.8)
            };
            return customers;
        }
    }




    // definition of Customer:
    public class Customer
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public double Balance { get; set; }

        public Customer(long id, string name, DateTime registrationDate, double balance)
        {
            this.Id = id;
            this.Name = name;
            this.RegistrationDate = registrationDate;
            this.Balance = balance;
        }
    }
}
