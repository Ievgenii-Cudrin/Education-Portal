using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.InstanceCreator;
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

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CourseViewModel, Course>());
            var mapper = new Mapper(config);
            // сопоставление
            var userMap = mapper.Map<CourseViewModel, Course>(course);
            //Create new user, if not - false
            bool createUser = courseService.CreateCourse(userMap);

            ProgramConsoleMessageHelper.
                ShowFunctionResult(
                createUser,
                "Course successfully created!",
                "Something wrong",
                ProgramBranch.SelectFirstStepForAuthorizedUser,
                ProgramBranch.SelectFirstStepForAuthorizedUser
                );
        }
    }
}
