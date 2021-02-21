﻿namespace EducationPortalConsoleApp.Controller
{
    using System;
    using System.Linq;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using EducationPortal.PL.InstanceCreator;
    using EducationPortal.PL.Interfaces;
    using EducationPortal.PL.Mapping;
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Branch;
    using EducationPortalConsoleApp.Interfaces;
    using Entities;

    public class CourseController : ICourseController
    {
        private ICourseService courseService;
        private IMapperService mapperService;
        private ISkillController skillController;

        public CourseController(
            ICourseService courseService,
            IMapperService mapper,
            ISkillController skilCntrl)
        {
            this.courseService = courseService;
            this.mapperService = mapper;
            this.skillController = skilCntrl;
        }

        public void CreateNewCourse()
        {
            Console.Clear();

            // Create course
            CourseViewModel courseVM = CourseVMInstanceCreator.CreateCourse();
            var course = this.mapperService.CreateMapFromVMToDomain<CourseViewModel, Course>(courseVM);
            // mapping to Domain model
            var courseDomain = this.mapperService.CreateMapFromVMToDomain<CourseViewModel, Course>(courseVM);
            bool success = this.courseService.CreateCourse(courseDomain);

            if (success)
            {
                // Add materials to course
                this.AddMaterialToCourse(courseDomain.Id);

                // Add skills to course
                this.AddSkillsToCourse(courseDomain.Id);

                // go ro start menu
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
            else
            {
                Console.WriteLine("Course exist");

                // Go to start menu
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
        }

        private void AddMaterialToCourse(int courseId)
        {
            string userChoice;
            do
            {
                Material materialDomain = ProgramBranch.SelectMaterialForAddToCourse(courseId);

                // check, material exist in course, or no
                if (!this.courseService.AddMaterialToCourse(courseId, materialDomain))
                {
                    Console.WriteLine("Material exist in course");
                }

                Console.WriteLine("Do you want to add more material (Enter YES)?");
                userChoice = Console.ReadLine();
            }
            while (userChoice.ToLower() == "yes");
        }

        private void AddSkillsToCourse(int courseId)
        {
            string userChoice;

            do
            {
                Console.WriteLine("Add skill to your course: ");

                // create skill
                var skillDomain = this.skillController.CreateSkill();

                // add skill to course after mapping
                this.courseService.AddSkillToCourse(courseId, skillDomain);
                Console.WriteLine("Do you want to add one more skill (Enter YES)?");
                userChoice = Console.ReadLine();
            }
            while (userChoice.ToLower() == "yes");
        }
    }
}
