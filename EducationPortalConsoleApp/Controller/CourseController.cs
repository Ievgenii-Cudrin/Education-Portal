using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.InstanceCreator;
using EducationPortal.PL.Mapping;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Helpers;
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
            Console.Clear();
            CourseViewModel course = CourseVMInstanceCreator.CreateCourse();
            List<MaterialViewModel> materials = new List<MaterialViewModel>();
            List
            //Create new course, if not - false
            bool createCourse = courseService.CreateCourse(Mapping.CreateMapFromVMToDomain<CourseViewModel, Course>(course));



            //ProgramConsoleMessageHelper.
            //    ShowFunctionResult(
            //    createCourse,
            //    "Course successfully created!",
            //    "Something wrong",
            //    ProgramBranch.SelectFirstStepForAuthorizedUser,
            //    ProgramBranch.SelectFirstStepForAuthorizedUser
            //    );
        }
    }
}
