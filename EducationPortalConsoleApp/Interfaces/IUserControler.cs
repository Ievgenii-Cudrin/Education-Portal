using EducationPortal.PL.Interfaces;
using System.Threading.Tasks;

namespace EducationPortalConsoleApp.Interfaces
{
    public interface IUserController
    {
        Task CreateNewUser();

        Task VerifyLoginAndPassword();

        void LogOut();

        Task ShowAllPassedCourses();

        Task ShowAllUserSkills();

        Task ShowAllCourseInProggres();

        void ShowUserInfo();

        void WithApplication(IApplication application);
    }
}
