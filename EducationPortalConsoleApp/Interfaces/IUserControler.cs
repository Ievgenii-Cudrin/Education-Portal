using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Interfaces
{
    public interface IUserController
    {
        public void CreateNewUser();

        public void VerifyLoginAndPassword();

        public void LogOut();

        public void ShowAllPassedCourses();

        public void ShowAllUserSkills();

        public void ShowAllCourseInProggres();

        public void ShowUserInfo();
    }
}
