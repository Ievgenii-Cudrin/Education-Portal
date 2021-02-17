namespace EducationPortalConsoleApp.Interfaces
{
    public interface IUserController
    {
        void CreateNewUser();

        void VerifyLoginAndPassword();

        void LogOut();

        void ShowAllPassedCourses();

        void ShowAllUserSkills();

        void ShowAllCourseInProggres();

        void ShowUserInfo();
    }
}
