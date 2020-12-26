using DataAccessLayer.Entities;
using DataAccessLayer.Serialization;
using System;
using System.Collections.Generic;

namespace EducationPortalConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UsersSerialization ser = new UsersSerialization();
            List<User> users = ser.GetAllUsers();

            //test cycle
            foreach(var user in users)
            {
                Console.WriteLine($"Name: {user.Name}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Phone Number: {user.PhoneNumber}");
            }

            //test cycle
            //while (true)
            //{
            //    Console.WriteLine("Enter your name: ");
            //    string _Name = Console.ReadLine();


            //    Console.WriteLine("Enter your Email: ");
            //    string _Email = Console.ReadLine();


            //    Console.WriteLine("Enter your PhoneNumber: ");
            //    string _PhoneNumber = Console.ReadLine();


            //    User user = new User()
            //    {
            //        Email = _Email,
            //        Name = _Name,
            //        PhoneNumber = _PhoneNumber
            //    };



            //    ser.AddObjectToXml(user);
            //}

            Console.ReadLine();
        }
    }
}
