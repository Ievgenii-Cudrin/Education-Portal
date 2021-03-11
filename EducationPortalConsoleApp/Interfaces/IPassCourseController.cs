using System.Threading.Tasks;

namespace EducationPortal.PL.Interfaces
{
    public interface IPassCourseController
    {
        Task StartPassCourse();

        Task StartPassingCourseFromProgressList();

        void WithApplication(IApplication application);
    }
}
