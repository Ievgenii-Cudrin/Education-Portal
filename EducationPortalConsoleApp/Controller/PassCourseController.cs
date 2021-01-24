using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.Interfaces;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EducationPortal.PL.Controller
{
    public class PassCourseController : IPassCourseController
    {
        ICourseService courseService;
        IMaterialController materialController;
        IUserService userService;

        public PassCourseController(ICourseService courseService, IMaterialController materialController, IUserService userService)
        {
            this.courseService = courseService;
            this.materialController = materialController;
            this.userService = userService;
        }

        public void StartPassingCourseFromProgressList()
        {
            List<CourseViewModel> coursesListInProgressVM = Mapping.Mapping.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(userService.GetListWithCoursesInProgress().ToList());

            foreach (var course in coursesListInProgressVM)
            {
                course.Materials = Mapping.Mapping.CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(courseService.GetMaterialsFromCourse(course.Id));
                course.Skills = Mapping.Mapping.CreateListMap<Skill, SkillViewModel>(courseService.GetSkillsFromCourse(course.Id));
            }
        }

        public void StartPassCourse()
        {
            //1
            List<CourseViewModel> coursesListVM = Mapping.Mapping.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(courseService.GetAllCourses().ToList());

            foreach (var course in coursesListVM)
            {
                course.Materials = Mapping.Mapping.CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(courseService.GetMaterialsFromCourse(course.Id));
                course.Skills = Mapping.Mapping.CreateListMap<Skill, SkillViewModel>(courseService.GetSkillsFromCourse(course.Id));
            }

            ShowCoursesToPass(coursesListVM);

            //2
            int courseIdToPass = GetIdFromUserToPassCourse();
            if(userService.GetListWithCoursesInProgress().Any(x => x.Id == courseIdToPass))
            {
                Console.WriteLine("You have already started this course");
                Thread.Sleep(4000);
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }

            CourseViewModel courseInProgress = coursesListVM.Where(x => x.Id == courseIdToPass).FirstOrDefault();

            if(courseInProgress != null)
            {
                Console.Clear();
                userService.AddCourseInProgress(courseInProgress.Id);
                Console.WriteLine("Let's start studying the materials." + 
                    "\n After reading, put + in front of the material." + 
                    "\n Any other value will not count the passage of the material \n");

                foreach(var material in courseInProgress.Materials)
                {
                    Console.WriteLine(material.ToString());
                    Console.Write("\nHas the material been studied (Enter '+') or exit from studying course (Enter 'Exit')?" );
                    string learnMaterial = Console.ReadLine();

                    if(learnMaterial == "+")
                    {
                        material.IsPassed = true;
                        userService.UpdateValueOfPassMaterialInProgress(courseIdToPass, material.Id);
                    }
                    else if(learnMaterial.ToLower() == "exit")
                    {
                        Console.WriteLine("Сourse not finished!");
                        break;
                    }
                }

                bool allMaterialsPassed = courseInProgress.Materials.All(x => x.IsPassed == true);

                if (allMaterialsPassed)
                {
                    userService.AddCourseToPassed(courseInProgress.Id);
                    userService.DeleteCourseFromProgress(courseInProgress.Id);

                    foreach(var skill in courseInProgress.Skills)
                    {
                        userService.AddSkill(Mapping.Mapping.CreateMapFromVMToDomain<SkillViewModel, Skill>(skill));
                    }

                    Console.WriteLine($"Congratulations, you have successfully completed the course {courseInProgress.Name}");
                    Thread.Sleep(4000);
                }
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }

            
        }

        public static int GetIdFromUserToPassCourse()
        {
            int id;
            Console.WriteLine("Please, enter course to pass");
            try
            {
                return id = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid data + {ex.Message}");
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
            return -1;
        }

        public void ShowCoursesToPass(List<CourseViewModel> coursesListVM)
        {
            foreach (var course in coursesListVM)
            {
                Console.WriteLine($"\n№ {course.Id} - {course.Name}");
                Console.WriteLine("This course includes such materials:");

                foreach (var material in course.Materials)
                {
                    Console.WriteLine(material.ToString());
                }

                Console.WriteLine("After completing this course, you will receive such skills: \n");

                foreach (var skill in course.Skills)
                {
                    Console.WriteLine($"{skill.Name}");
                }
            }
        }
    }
}
