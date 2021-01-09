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
            Name = GetDataHelper.GetNameFromUser(),
            Email = GetDataHelper.GetEmailFromUser(),
            PhoneNumber = GetDataHelper.GetPhoneNumberFromUser()
        };
    }
}
