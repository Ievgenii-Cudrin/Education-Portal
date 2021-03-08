namespace EducationPortal.PL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPassCourseController
    {
        Task StartPassCourse();

        Task StartPassingCourseFromProgressList();

        void WithApplication(IApplication application);
    }
}
