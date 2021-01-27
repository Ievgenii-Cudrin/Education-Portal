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
            CourseViewModel courseVM = CourseVMInstanceCreator.CreateCourse();
            var courseDomain = Mapping.CreateMapFromVMToDomain<CourseViewModel, Course>(courseVM);
            bool success = courseService.CreateCourse(courseDomain);

            if (success)
            {
                int courseId = courseService.GetAllCourses().Where(x => x.Name == courseVM.Name).FirstOrDefault().Id;
                string userChoice = String.Empty;

                //transfer to another method
                do
                {
                    Material materialDomain = ProgramBranch.SelectMaterialForAddToCourse();
                    if(!courseService.AddMaterialToCourse(courseId, materialDomain))
                    {
                        Console.WriteLine("Material exist in course");
                    }
                    Console.WriteLine("Do you want to add more material (Enter YES)?");
                    userChoice = Console.ReadLine();
                }
                while (userChoice.ToLower() == "yes");

                //transfer to another method
                do
                {
                    Console.WriteLine("Add skill to your course: ");
                    var skillVM = SkillVMInstanceCreator.CreateSkill();
                    courseService.AddSkillToCourse(courseId, Mapping.CreateMapFromVMToDomain<SkillViewModel, Skill>(skillVM));
                    Console.WriteLine("Do you want to add one more skill (Enter YES)?");
                    userChoice = Console.ReadLine();
                }
                while (userChoice.ToLower() == "yes");

                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
            else
            {
                Console.WriteLine("Course exist");
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
        }
    }
}
