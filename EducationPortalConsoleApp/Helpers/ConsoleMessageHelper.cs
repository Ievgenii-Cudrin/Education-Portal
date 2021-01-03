using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Helpers
{
    public class ConsoleMessageHelper
    {
        public static void ShowTextForChoice()
        {
            Console.WriteLine($"Hi, dear user. Please, make your choice: " +
            $"\n1.Create user" +
            $"\n2.Update user" +
            $"\n3.Show all users" +
            $"\n4.Delete user");
        }

        public static void ShowObjects(List<User> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine($"Name: {user.Name}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Phone number: {user.PhoneNumber}");
                Console.WriteLine("---------------------------");
            }
        }
    }
}
