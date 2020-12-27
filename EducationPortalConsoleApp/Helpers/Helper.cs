using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EducationPortalConsoleApp.Helpers
{
    public static class Helper
    {
        public static string GetPhoneNumberFromUser()
        {
        EnterPhoneNumber:
            Console.WriteLine($"Enter phone number: ");
            string phoneNumber = Console.ReadLine();

            if (Regex.IsMatch(phoneNumber, @"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", RegexOptions.IgnoreCase))
            {
                Console.WriteLine("Incorrect phone number. Please, try again!");
                goto EnterPhoneNumber;
            }
            return phoneNumber;
        }

        public static string GetEmailFromUser()
        {
        EnterEmail:
            Console.WriteLine($"Enter email: ");
            string email = Console.ReadLine();

            //check this expression
            if (!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                Console.WriteLine("Incorrect email. Please, try again!");
                goto EnterEmail;
            }
            return email;
        }

        public static string GetNameFromUser()
        {
        EnterName:
            Console.WriteLine($"Enter name: ");
            string name = Console.ReadLine();

            if (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name must not be empty. Please, try again!");
                goto EnterName;
            }
            return name;
        }

        public static void ShowTextForChoice()
        {
            Console.WriteLine($"Hi, dear user. Please, make your choice: " +
            $"\n1.Create user" +
            $"\n2.Update user" +
            $"\n3.Show all users" +
            $"\n4.Delete user");
        }

        public static User GenerateObjectFromUser() => new User()
        {
            Name = Helper.GetNameFromUser(),
            Email = Helper.GetEmailFromUser(),
            PhoneNumber = Helper.GetPhoneNumberFromUser()
        };
    }
}
