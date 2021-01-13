using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.InstanceCreator
{
    public static class UserInstanceCreator
    {
        public static User UserCreator(string name, string password, string email, string phoneNumber)
        {
            User user = null;

            if (name != null && password != null && email != null && phoneNumber != null)
            {
                user = new User()
                {
                    Name = name,
                    Password = password,
                    Email = email,
                    PhoneNumber = phoneNumber
                };
            }

            return user == null ? null : user;
        }
    }
}
