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
            //1
            Customer firstRegistratedCustomer = WorkWithData.GetFirstRegistratedCistomer();

            //2
            double balanceAverageOfAllCustomers = WorkWithData.GetAverageByBalanceAllCustomer();

            //3
            IEnumerable<Customer> filterByDate = WorkWithData.GetFilteredByDateCustomers();

            //4
            IEnumerable<Customer> filterCustomerById = WorkWithData.GetCustomerById();

            //5
            IEnumerable<Customer> filteredCustomerByPartOfName = WorkWithData.GetFilterByPartOfNameCustomer();

            //6
            WorkWithData.GroupByMonth();

            //7
            IEnumerable<Customer> sortedCustonerByPropertyFromUser = WorkWithData.ShowCustomerBySomeProperty();

            //8
            WorkWithData.ShowAllNames();

            Console.ReadLine();
        }
    }
}
