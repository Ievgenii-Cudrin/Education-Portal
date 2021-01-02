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
                Id = "728",
                Name = "Jeffrey Richter obj 728",
                Email = "728-Kto-to-noviy@gmail.com",
                PhoneNumber = "321654"
            };

            //serialize.Add(user);

            //User user2 = serialize.Get(722);

            IEnumerable<User> users = serialize.GetAll();

            //serialize.Delete(722);

            serialize.UpdateObject(user);

            //new ProgramService().StartApp();

            Console.ReadLine();
        }
    }
}
