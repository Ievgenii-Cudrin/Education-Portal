using Entities;
using System.Threading.Tasks;

namespace EducationPortal.PL.Interfaces
{
    public interface IApplication
    {
        Task StartApplication();

        Task SelectFirstStepForAuthorizedUser();

        Task<Material> SelectMaterialForAddToCourse(int courseId);
    }
}
