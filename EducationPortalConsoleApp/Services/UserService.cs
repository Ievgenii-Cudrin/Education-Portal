﻿using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EducationPortalConsoleApp.Services
{
    public class UserService
    {
        IUnitOfWork _uow;
        public UserService()
        {
            this._uow = new EFUnitOfWork();
        }
        public void StartApp()
        {
            ShowTextForChoice();

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    CreateUser();
                    break;
                case "2":
                    UpdateUser();
                    break;
                case "3":
                    ShowAllUsers();
                    break;
                case "4":
                    DeleteUser();
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            void CreateUser()
            {
                _uow.Users.Create(GetDataFromUserHelper.GenerateObjectFromUser());
            }

            void UpdateUser()
            {
                Console.Write($"Enter user ID to update: ");
                int id = Convert.ToInt32(Console.ReadLine());

                User user = _uow.Users.Get(id);
                if (user == null)
                {
                    Console.WriteLine($"User not found");
                }
                else
                {
                    user.Name = GetDataFromUserHelper.GetNameFromUser();
                    user.Email = GetDataFromUserHelper.GetEmailFromUser();
                    user.PhoneNumber = GetDataFromUserHelper.GetPhoneNumberFromUser();

                    _uow.Users.Update(user);
                }

            }

            void ShowAllUsers()
            {
                IEnumerable<User> users = _uow.Users.GetAll();
                
            }

            void DeleteUser()
            {
                Console.Write($"Enter user ID to delete: ");
                int id = Convert.ToInt32(Console.ReadLine());

                User user = _uow.Users.Get(id);
                if (user == null)
                {
                    Console.WriteLine($"User not found");
                }
                else
                {
                    _uow.Users.Delete(Convert.ToInt32(user.Id));
                }
            }
        }
    }
}