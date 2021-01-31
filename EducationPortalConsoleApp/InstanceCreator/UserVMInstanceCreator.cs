using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.InstanceCreator
{
    public static class UserVMInstanceCreator
    {
        public static UserViewModel CreateUser()
        {
            UserViewModel user = new UserViewModel()
            {
                Name = GetDataHelper.GetNameFromUser(),
                Email = GetDataHelper.GetEmailFromUser(),
                Password = GetDataHelper.GetPasswordWithConfirmFromUser(),
                PhoneNumber = GetDataHelper.GetPhoneNumberFromUser()
            };

            return user;
        }
    }
}
