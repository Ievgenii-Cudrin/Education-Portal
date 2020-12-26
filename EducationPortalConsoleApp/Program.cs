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
            //ser.DeleteUser("a");


            //test AddUser method
            while (true)
            {
                Console.WriteLine("Enter your name: ");
                string _Name = Console.ReadLine();


                Console.WriteLine("Enter your Email: ");
                string _Email = Console.ReadLine();


                Console.WriteLine("Enter your PhoneNumber: ");
                string _PhoneNumber = Console.ReadLine();


                User user = new User()
                {
                    Email = _Email,
                    Name = _Name,
                    PhoneNumber = _PhoneNumber
                };



                ser.AddObjectToXml(user);
                //List<User> users = ser.GetAllUsers();
                //foreach (var user1 in users)
                //{
                //    Console.WriteLine($"Name: {user1.Name}");
                //    Console.WriteLine($"Email: {user1.Email}");
                //    Console.WriteLine($"Phone Number: {user1.PhoneNumber}");
                //}
            }

            Console.ReadLine();
        }
    }
}
