using BusinessLogicLayer.Interfaces;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Controller
{
    public class CourseController : ICourseController
    {
        ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public void CreateNewCourse()
        {
            CourseViewModel course = CourseInstanceCreator
        }
    }
}
