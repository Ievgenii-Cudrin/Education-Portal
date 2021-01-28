using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.InstanceCreator;
using EducationPortal.PL.Mapping;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Interfaces;
using System.Linq;
using System;
using EducationPortalConsoleApp.Branch;

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
            //Create course
            CourseViewModel courseVM = CourseVMInstanceCreator.CreateCourse();
            //mapping to Domain model
            var courseDomain = Mapping.CreateMapFromVMToDomain<CourseViewModel, Course>(courseVM);
            bool success = courseService.CreateCourse(courseDomain);

            if (success)
            {
                int courseId = courseService.GetAllCourses().Where(x => x.Name == courseVM.Name).FirstOrDefault().Id;
                //Add materials to course
                AddMaterialToCourse(courseId);
                //Add skills to course
                AddSkillsToCourse(courseId);
                //go ro start menu
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
            else
            {
                Console.WriteLine("Course exist");
                //Go to start menu
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
        }

        private void AddMaterialToCourse(int courseId)
        {
            string userChoice = String.Empty;

            do
            {
                Material materialDomain = ProgramBranch.SelectMaterialForAddToCourse();

                //check, material exist in course, or no
                if (!courseService.AddMaterialToCourse(courseId, materialDomain))
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
            string userChoice = String.Empty;

            do
            {
                Console.WriteLine("Add skill to your course: ");
                //create skill
                var skillVM = SkillVMInstanceCreator.CreateSkill();
                //add skill to course after mapping
                courseService.AddSkillToCourse(courseId, Mapping.CreateMapFromVMToDomain<SkillViewModel, Skill>(skillVM));
                Console.WriteLine("Do you want to add one more skill (Enter YES)?");
                userChoice = Console.ReadLine();
            }
            while (userChoice.ToLower() == "yes");
        }
    }
}
