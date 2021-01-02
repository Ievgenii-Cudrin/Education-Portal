using DataAccessLayer.Entities;
using DataAccessLayer.Serialization;
using EducationPortalConsoleApp.Services;
using System;
using System.Collections.Generic;

namespace EducationPortalConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UserSerialization<User> serialize = new UserSerialization<User>();

            User user = new User()
            {
                Name = "Jeffrey Richter",
                Email = "Kto-to-noviy@gmail.com",
                PhoneNumber = "321654"
            };

            serialize.AddObjectToXml(user);
            //new ProgramService().StartApp();

            Console.ReadLine();
        }
    }
}
