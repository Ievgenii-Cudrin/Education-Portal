namespace EducationPortal.PL.InstanceCreator
{
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Helpers;

    public static class UserVMInstanceCreator
    {
        public static UserViewModel CreateUser()
        {
            UserViewModel user = new UserViewModel()
            {
                Name = GetDataHelper.GetNameFromUser(),
                Email = GetDataHelper.GetEmailFromUser(),
                Password = GetDataHelper.GetPasswordWithConfirmFromUser(),
                PhoneNumber = GetDataHelper.GetPhoneNumberFromUser(),
            };

            return user;
        }
    }
}
