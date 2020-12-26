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

            //testing AddUser method
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

            //testing Delete method
            Console.WriteLine("Enter name to delete from database: ");
            string _Name = Console.ReadLine();
            ser.DeleteUser(_Name);

            Console.ReadLine();
        }
    }
}
