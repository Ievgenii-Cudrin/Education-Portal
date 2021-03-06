using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.Helpers;
using EducationPortal.PL.Interfaces;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Interfaces;
using Entities;

namespace EducationPortal.PL.Controller
{
    public class PassCourseController : IPassCourseController
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly IMaterialController materialController;
        private IMapperService mapperService;
        private IMaterialService materialService;

        public PassCourseController(
            ICourseService courseService,
            IUserService userService,
            IMaterialController materialController,
            IMapperService mapper,
            IMaterialService materialService)
        {
            this.courseService = courseService;
            this.userService = userService;
            this.materialController = materialController;
            this.mapperService = mapper;
            this.materialService = materialService;
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

        public void StartPassingCourseFromProgressList()
        {
            // Get all courses in progress
            List<CourseViewModel> coursesListInProgressVM = this.GetListOfCoursesFromServiceAfterMappingToVM(this.userService.GetListWithCoursesInProgress().ToList());

            // CHeck, we have courses in progress, or no
            if (coursesListInProgressVM.Count <= 0)
            {
                this.ShowMessageAndReturnToMainMenu("You have no unfinished courses!");
            }

            // Show all courses in progress
            this.ShowCoursesToPass(coursesListInProgressVM);

            // get course id from user to continue
            int courseInProgressIdToPass = GetIdFromUserToPassCourse();

            // checking valid course id
            if (!coursesListInProgressVM.Any(x => x.Id == courseInProgressIdToPass))
            {
                this.ShowMessageAndReturnToMainMenu("Invalid ID.");
            }

            // get course in progress
            var courseToProgressFromProcessList = this.userService.GetListWithCoursesInProgress().Where(x => x.Id == courseInProgressIdToPass).FirstOrDefault();

            // mapping to course domain to course view model
            CourseViewModel courseVMInProgress = this.mapperService.CreateMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(courseToProgressFromProcessList);

            // mapping materials in progogress
            courseVMInProgress.Materials = this.materialController.GetAllMaterialVMAfterMappingFromMaterialDomain(this.userService.GetMaterialsFromCourseInProgress(courseVMInProgress.Id));

            // mapping skills
            courseVMInProgress.Skills = this.mapperService.CreateListMap<Skill, SkillViewModel>(this.userService.GetSkillsFromCourseInProgress(courseVMInProgress.Id));

            if (courseVMInProgress != null)
            {
                Console.Clear();

                // List<MaterialViewModel> materials = materialController.GetAllMaterialVMAfterMappingFromMaterialDomain(courseToProgressFromProcessList.Materials.Select(x => x).Where(x => x.IsPassed == false).ToList());
                List<MaterialViewModel> materials = courseVMInProgress.Materials.Where(x => x.IsPassed == false).ToList();

                // Add to method course from processing list, and materials from this course, which do not passed
                this.PassingCourse(ref courseVMInProgress, materials);

                // checking if all materials have been worked out
                bool allMaterialsPassed = courseVMInProgress.Materials.All(x => x.IsPassed == true);

                if (allMaterialsPassed)
                {
                    // if all materials passed => add skills for user and add course to Passed list, delete course from processing list
                    this.WorkAfterSuccessfullyPassingCourse(allMaterialsPassed, courseVMInProgress);
                }
                else
                {
                    Console.WriteLine($"You do not passed all materials in course. Please, continue later.");
                    Thread.Sleep(4000);
                }

                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
        }

        public void StartPassCourse()
        {
            // Get all courses
            List<CourseViewModel> coursesListVM = this.GetListOfCoursesFromServiceAfterMappingToVM(this.userService.GetAvailableCoursesForUser());

            if (coursesListVM.Count <= 0)
            {
                this.ShowMessageAndReturnToMainMenu("No courses available");
            }

            // Show all courses
            this.ShowCoursesToPass(coursesListVM);

            // Get course id from user to passing
            int courseIdToPass = GetIdFromUserToPassCourse();

            // Check, may be we this course course already started
            if (this.userService.GetListWithCoursesInProgress().Any(x => x.Id == courseIdToPass)) // transfer 
            {
                this.ShowMessageAndReturnToMainMenu("You have already started this course");
            }

            if (!coursesListVM.Any(x => x.Id == courseIdToPass))
            {
                this.ShowMessageAndReturnToMainMenu("Сourses with this id do not exist");
            }

            // Greate course to continue passing
            CourseViewModel courseInProgress = coursesListVM.Where(x => x.Id == courseIdToPass).FirstOrDefault();

            if (courseInProgress != null)
            {
                Console.Clear();

                // add course to Progress List
                this.userService.AddCourseInProgress(courseInProgress.Id);
                PassCourseConsoleMessageHelper.ShowInfoToStartPassingCourse();

                // passing the course
                this.PassingCourse(ref courseInProgress, courseInProgress.Materials.ToList());

                // checking if all materials have been worked out
                bool allMaterialsPassed = courseInProgress.Materials.All(x => x.IsPassed == true);

                // add skills for user and add course to Passed list
                this.WorkAfterSuccessfullyPassingCourse(allMaterialsPassed, courseInProgress);
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
        }

        public void ShowCoursesToPass(List<CourseViewModel> coursesListVM)
        {
            foreach (var course in coursesListVM)
            {
                // show course name
                Console.WriteLine($"\n№ {course.Id} - {course.Name}");
                Console.WriteLine("This course includes such materials:");

                // Show info about all materials
                foreach (var material in course.Materials)
                {
                    Console.WriteLine(material.ToString());
                }

                Console.WriteLine("After completing this course, you will receive such skills: \n");

                // Show all skills in course
                foreach (var skill in course.Skills)
                {
                    Console.WriteLine($"{skill.Name}");
                }
            }
        }

        private void ShowMessageAndReturnToMainMenu(string message)
        {
            // Show message with error and return to main menu
            Console.WriteLine(message);
            Thread.Sleep(4000);
            ProgramBranch.SelectFirstStepForAuthorizedUser();
        }

        private void PassingCourse(ref CourseViewModel courseInProgress, List<MaterialViewModel> materials)
        {
            foreach (var material in materials)
            {
                // Show info about material
                Console.WriteLine(material.ToString());
                Console.Write("\nHas the material been studied (Enter '+') or exit from studying course (Enter 'Exit')?");

                // get value from user
                string learnMaterial = Console.ReadLine();

                if (learnMaterial == "+")
                {
                    // select true to passed material
                    courseInProgress.Materials.Select(x => x).Where(x => x.Name == material.Name).FirstOrDefault().IsPassed = true;

                    // update user with new value
                    this.userService.UpdateValueOfPassMaterialInProgress(courseInProgress.Id, material.Id);
                }
                else if (learnMaterial.ToLower() == "exit")
                {
                    Console.WriteLine("Сourse not finished!");
                    break;
                }
            }
        }

        private List<CourseViewModel> GetListOfCoursesFromServiceAfterMappingToVM(List<Course> courses)
        {
            // Mapping entity to view model, and mapping include entities lists
            List<CourseViewModel> coursesListVM = this.mapperService.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(courses);

            foreach (var course in coursesListVM)
            {
                course.Materials = this.mapperService.CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(this.courseService.GetMaterialsFromCourse(course.Id));
                course.Skills = this.mapperService.CreateListMap<Skill, SkillViewModel>(this.courseService.GetSkillsFromCourse(course.Id));
            }

            return coursesListVM;
        }

        private void WorkAfterSuccessfullyPassingCourse(bool allMaterialsPassed, CourseViewModel successfullyСompletedСourse)
        {
            if (allMaterialsPassed)
            {
                // add course to Passed List
                this.userService.AddCourseToPassed(successfullyСompletedСourse.Id);

                // add skills to user
                foreach (var skill in successfullyСompletedСourse.Skills)
                {
                    this.userService.AddSkill(this.mapperService.CreateMapFromVMToDomain<SkillViewModel, Skill>(skill));
                }

                Console.WriteLine($"Congratulations, you have successfully completed the course {successfullyСompletedСourse.Name}");
                Thread.Sleep(4000);
            }
        }
    }
}
