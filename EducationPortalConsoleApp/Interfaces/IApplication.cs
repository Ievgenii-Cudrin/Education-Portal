using Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
