namespace EducationPortalConsoleApp.Interfaces
{
    using EducationPortal.PL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICourseController
    {
        Task CreateNewCourse();

        void WithApplication(IApplication application);
    }
}
