using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.InstanceCreator
{
    public static class CourseVMInstanceCreator
    {
        public static CourseViewModel CreateCourse()
        {
            CourseViewModel user = new CourseViewModel()
            {
                Name = GetDataHelper.GetNameFromUser(),
                Description = GetDataHelper.GetDescriptionForCourseFromUser()
            };

            return user;
        }
    }
}
