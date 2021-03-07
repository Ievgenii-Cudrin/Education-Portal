namespace EducationPortalConsoleApp.Helpers
{
    using System;
    using System.Collections.Generic;
    using DataAccessLayer.Entities;
    using EducationPortal.PL.Models;

    public class UserConsoleMessageHelper
    {
        public static void ShowTextForChoice()
        {
            Console.WriteLine($"Okey. Make the next choice to continue: " +
            $"\n1.Create user" +
            $"\n2.Update user" +
            $"\n3.Show all users" +
            $"\n4.Delete user" +
            $"\n5.Return");
        }

        public static void ShowObjects(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id}");
                Console.WriteLine($"Name: {user.Name}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Phone number: {user.PhoneNumber}");
                Console.WriteLine("---------------------------");
            }
        }

        public static void ShowInfoAboutCourses(List<CourseViewModel> courses)
        {
            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}.Name - {courses[i].Name}, ID - {courses[i].Id}");
            }
        }
    }
}
