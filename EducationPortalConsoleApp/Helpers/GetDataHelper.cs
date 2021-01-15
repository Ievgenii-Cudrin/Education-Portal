using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EducationPortalConsoleApp.Helpers
{
    public static class GetDataHelper
    {
        public static string GetPhoneNumberFromUser()
        {
            string phoneNumber;
            bool valdiPhoneNumber = false;
            do
            {
                Console.WriteLine($"Enter phone number (0999339210): ");
                phoneNumber = Console.ReadLine();

                valdiPhoneNumber = Regex.IsMatch(phoneNumber, @"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", RegexOptions.IgnoreCase);
                if (!valdiPhoneNumber)
                {
                    Console.WriteLine("Incorrect phone number. Please, try again!");
                }
            }
            while (!valdiPhoneNumber);
            
            return phoneNumber;
        }

        public static string GetEmailFromUser()
        {
            string email;
            bool validEmail = true;
            do
            {
                Console.WriteLine($"Enter email: ");
                email = Console.ReadLine();
                validEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                //check this expression
                if (!validEmail)
                {
                    Console.WriteLine("Incorrect email. Please, try again!");
                }
            }
            while (!validEmail);
            
            return email;
        }

        public static string GetNameFromUser()
        {
            string name;
            bool validName = true;
            do
            {
                Console.WriteLine($"Enter name: ");
                name = Console.ReadLine();
                validName = String.IsNullOrEmpty(name);
                if (validName)
                {
                    Console.WriteLine("Name must not be empty. Please, try again!");
                }
            }
            while (validName);
            return name;
        }

        public static string GetPasswordWithConfirmFromUser()
        {
            string password = "";
            string confirmPassword = "";
            bool goodPassword;
            do
            {
                Console.WriteLine($"Enter password: ");
                password = Console.ReadLine();
                Console.WriteLine($"Confirm password: ");
                confirmPassword = Console.ReadLine();
                goodPassword = !String.IsNullOrEmpty(password) && password == confirmPassword;
                if (!goodPassword)
                {
                    Console.WriteLine("Password must not be empty and passwords must match. Please, try again!");
                }
            }
            while (!goodPassword);
            return password;
        }

        public static string GetPasswordFromUser()
        {
            string password = "";
            bool goodPassword;
            do
            {
                Console.WriteLine($"Enter password: ");
                password = Console.ReadLine();
                goodPassword = String.IsNullOrEmpty(password);
                if (goodPassword)
                {
                    Console.WriteLine("Password must not be empty and passwords must match. Please, try again!");
                }
            }
            while (goodPassword);
            return password;
        }

        public static DateTime GetDateTimeFromUser()
        {
            DateTime userDateTime;
            bool validDate = false;
            do
            {
                Console.WriteLine("Enter a date (e.g. 22.10.1987): ");

                validDate = DateTime.TryParse(Console.ReadLine(), out userDateTime);
                if (validDate)
                {
                    return userDateTime;
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect value.");
                }
            }
            while (!validDate);

            return userDateTime;
            
        }

        public static string GetSiteAddressFromUser()
        {
            string site;
            bool validSite;
            do
            {
                Console.WriteLine($"Enter site (e.g. www.google.com): ");
                site = Console.ReadLine();
                validSite = Regex.IsMatch(site, @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$", RegexOptions.IgnoreCase);
                if (!validSite)
                {
                    Console.WriteLine("Site address must not be empty. Please, try again!");
                }
            }
            while (!validSite);
            
            return site;
        }

        public static string GetAuthorNameFromUser()
        {
            string authorName;
            bool isNullOrEmpty;
            do
            {
                Console.WriteLine($"Enter author name: ");
                authorName = Console.ReadLine();
                isNullOrEmpty = String.IsNullOrEmpty(authorName);
                if (isNullOrEmpty)
                {
                    Console.WriteLine("Name must not be empty. Please, try again!");
                }
            }
            while (isNullOrEmpty);
            
            return authorName;
        }

        public static int GetCountOfBookPages()
        {
            int countOfPages = 0;
            do
            {
                Console.WriteLine($"Enter count of book pages: ");
                countOfPages = Convert.ToInt32(Console.ReadLine());

                if (countOfPages < 170)
                {
                    Console.WriteLine("The number of book pages must be more than 170. Please, try again!");
                }
            }
            while (countOfPages < 170);
            
            return countOfPages;
        }

        public static int GetVideoDuration()
        {
            int videoDuration = 0;
            do
            {
                Console.WriteLine($"Enter video duration in minutes: ");
                videoDuration = Convert.ToInt32(Console.ReadLine());

                if (videoDuration < 1)
                {
                    Console.WriteLine("Video duration in minutes must be more than 1. Please, try again!");
                }
            }
            while (videoDuration < 1);
            
            return videoDuration;
        }

        public static int GetVideoQuality()
        {
            int videoQuality;
            bool validVideoQuality;
            do
            {

                Console.WriteLine($"Enter video quality: ");
                videoQuality = Convert.ToInt32(Console.ReadLine());
                validVideoQuality = videoQuality > 144 && videoQuality < 1080;
                if (validVideoQuality)
                {
                    Console.WriteLine("video quality should be in the range from 144 to 1080. Please, try again!");
                }
            }
            while(validVideoQuality);
            
            return videoQuality;
        }
    }
}
