using EducationPortal.PL.Interfaces;
using System.Threading.Tasks;

namespace EducationPortalConsoleApp.Interfaces
{
    public interface ICourseController
    {
        Task CreateNewCourse();

        void WithApplication(IApplication application);
    }
}
