using DataAccessLayer.Entities;
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

        public static DateTime GetDateTimeFromUser()
        {
            TrySetNewDate:
            Console.WriteLine("Enter a date (e.g. 10/22/1987): ");
            DateTime userDateTime;
            if (DateTime.TryParse(Console.ReadLine(), out userDateTime))
            {
                return userDateTime;
            }
            else
            {
                Console.WriteLine("You have entered an incorrect value.");
                goto TrySetNewDate;
            }
        }

        public static string GetSiteAddressFromUser()
        {
            EnterName:
            Console.WriteLine($"Enter site: ");
            string site = Console.ReadLine();

            if (String.IsNullOrEmpty(site))
            {
                Console.WriteLine("Site address must not be empty. Please, try again!");
                goto EnterName;
            }
            return site;
        }

        public static string GetAuthorNameFromUser()
        {
            EnterAuthorName:
            Console.WriteLine($"Enter author name: ");
            string authorName = Console.ReadLine();

            if (String.IsNullOrEmpty(authorName))
            {
                Console.WriteLine("Name must not be empty. Please, try again!");
                goto EnterAuthorName;
            }
            return authorName;
        }

        public static int GetCountOfBookPages()
        {
            EnterCountOfPages:
            Console.WriteLine($"Enter count of book pages: ");
            int countOfPages = Convert.ToInt32(Console.ReadLine());

            if (countOfPages < 1)
            {
                Console.WriteLine("The number of book pages must be more than 1. Please, try again!");
                goto EnterCountOfPages;
            }
            return countOfPages;
        }

        public static int GetDurationOfVideo()
        {
            EnterCountOfPages:
            Console.WriteLine($"Enter video duration in minutes: ");
            int videoDuration = Convert.ToInt32(Console.ReadLine());

            if (videoDuration < 1)
            {
                Console.WriteLine("Video duration in minutes must be more than 1. Please, try again!");
                goto EnterCountOfPages;
            }
            return videoDuration;
        }

        public static string GetVideoQuality()
        {
            EnterQuality:
            Console.WriteLine($"Enter video quality: ");
            string videoQuality = Console.ReadLine();

            if (String.IsNullOrEmpty(videoQuality))
            {
                Console.WriteLine("Video quality must not be empty. Please, try again!");
                goto EnterQuality;
            }
            return videoQuality;
        }
    }
}
