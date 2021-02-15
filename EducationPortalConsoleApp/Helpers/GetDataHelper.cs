namespace EducationPortalConsoleApp.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using DataAccessLayer.Interfaces;
    using EducationPortal.PL.EnumViewModel;

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

                // check this expression
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
                validName = string.IsNullOrEmpty(name);
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
            string password = string.Empty;
            string confirmPassword = string.Empty;
            bool goodPassword;

            do
            {
                Console.WriteLine($"Enter password: ");
                password = Console.ReadLine();
                Console.WriteLine($"Confirm password: ");
                confirmPassword = Console.ReadLine();
                goodPassword = !string.IsNullOrEmpty(password) && password == confirmPassword;
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
            string password = string.Empty;
            bool goodPassword;
            do
            {
                Console.WriteLine($"Enter password: ");
                password = Console.ReadLine();
                goodPassword = string.IsNullOrEmpty(password);
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
                isNullOrEmpty = string.IsNullOrEmpty(authorName);
                if (isNullOrEmpty)
                {
                    Console.WriteLine("Name must not be empty. Please, try again!");
                }
            }
            while (isNullOrEmpty);

            return authorName;
        }

        public static string GetDescriptionForCourseFromUser()
        {
            string description;
            bool isNullOrEmpty;
            do
            {
                Console.WriteLine($"Enter description for course: ");
                description = Console.ReadLine();
                isNullOrEmpty = string.IsNullOrEmpty(description);
                if (isNullOrEmpty)
                {
                    Console.WriteLine("Name must not be empty. Please, try again!");
                }
            }
            while (isNullOrEmpty);

            return description;
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

        public static VideoQualityViewModel GetVideoQuality()
        {
            VideoQualityViewModel videoQuality = VideoQualityViewModel.P360;
            bool validVideoQuality = false;
            do
            {
                Console.WriteLine($"Enter video quality (e. g. 144, 240, 360, 480, 720, 1080, 1440, 2160): ");
                string qualityFromUser = Console.ReadLine();

                switch (qualityFromUser)
                {
                    case "144":
                        videoQuality = VideoQualityViewModel.P144;
                        return videoQuality;
                    case "240":
                        videoQuality = VideoQualityViewModel.P240;
                        return videoQuality;
                    case "360":
                        videoQuality = VideoQualityViewModel.P360;
                        return videoQuality;
                    case "480":
                        videoQuality = VideoQualityViewModel.P480;
                        return videoQuality;
                    case "720":
                        videoQuality = VideoQualityViewModel.P720;
                        return videoQuality;
                    case "1080":
                        videoQuality = VideoQualityViewModel.P1080;
                        return videoQuality;
                    case "1440":
                        videoQuality = VideoQualityViewModel.P1440;
                        return videoQuality;
                    case "2160":
                        videoQuality = VideoQualityViewModel.P2160;
                        return videoQuality;
                    default:
                        Console.WriteLine("Invalid video quality");
                        break;
                }
            }
            while (!validVideoQuality);

            return videoQuality;
        }
    }
}
