using DataAccessLayer.Entities;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.InstanceCreator
{
    public static class UserInstanceCreator
    {
        public static User UserCreator() => new User()
        {
            Name = UserGetDataHelper.GetNameFromUser(),
            Email = UserGetDataHelper.GetEmailFromUser(),
            PhoneNumber = UserGetDataHelper.GetPhoneNumberFromUser()
        };
    }
}
