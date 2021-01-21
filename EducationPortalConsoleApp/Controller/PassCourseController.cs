using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.Interfaces;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationPortal.PL.Controller
{
    public class PassCourseController : IPassCourseController
    {
        ICourseService courseService;
        IMaterialController materialController;

        public void PassCourse(ICourseService courseService, IMaterialController materialController)
        {
            this.courseService = courseService;
            this.materialController = materialController;
        }

        public void StartPassCourse()
        {
            List<MaterialViewModel> materialsVM1 = materialController.GetAllMaterialVMAfterMappingFromMaterialDomain();
            
            List<CourseViewModel> coursesListVM = Mapping.Mapping.CreateListMap<Course, CourseViewModel>(courseService.GetAllCourses().ToList());
        }
    }
}
