using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.Helpers;
using EducationPortal.PL.Interfaces;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Interfaces;
using Entities;
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
        IUserService userService;
        IMaterialController materialController;

        public PassCourseController(ICourseService courseService, IUserService userService, IMaterialController materialController)
        {
            this.courseService = courseService;
            this.userService = userService;
            this.materialController = materialController;
        }

        public void StartPassingCourseFromProgressList()
        {
            //Get all courses in progress
            List<CourseViewModel> coursesListInProgressVM = GetListOfCoursesFromServiceAfterMappingToVM(userService.GetListWithCoursesInProgress().ToList());

            //CHeck, we have courses in progress, or no
            if(coursesListInProgressVM.Count <= 0)
            {
                ShowMessageAndReturnToMainMenu("You have no unfinished courses!");
            }

            //Show all courses in progress
            ShowCoursesToPass(coursesListInProgressVM);
            //get course id from user to continue 
            int courseInProgressIdToPass = GetIdFromUserToPassCourse();

            //checking valid course id
            if (!coursesListInProgressVM.Any(x => x.Id == courseInProgressIdToPass))
            {
                ShowMessageAndReturnToMainMenu("Invalid ID.");
            }

            //get course in progress
            var courseToProgressFromProcessList = userService.GetListWithCoursesInProgress().Where(x => x.Id == courseInProgressIdToPass).FirstOrDefault();
            //mapping to course domain to course view model
            CourseViewModel courseVMInProgress = Mapping.Mapping.CreateMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(courseToProgressFromProcessList);
            //mapping materials in progogress
            courseVMInProgress.Materials = materialController.GetAllMaterialVMAfterMappingFromMaterialDomain(userService.GetMaterialsFromCourseInProgress(courseVMInProgress.Id));
            //mapping skills
            courseVMInProgress.Skills = Mapping.Mapping.CreateListMap<Skill, SkillViewModel>(userService.GetSkillsFromCourseInProgress(courseVMInProgress.Id));

            if(courseVMInProgress != null)
            {
                Console.Clear();

                //List<MaterialViewModel> materials = materialController.GetAllMaterialVMAfterMappingFromMaterialDomain(courseToProgressFromProcessList.Materials.Select(x => x).Where(x => x.IsPassed == false).ToList());
                List<MaterialViewModel> materials = courseVMInProgress.Materials.Where(x => x.IsPassed == false).ToList();
                //Add to method course from processing list, and materials from this course, which do not passed
                PassingCourse(ref courseVMInProgress, materials);
                //checking if all materials have been worked out
                bool allMaterialsPassed = courseVMInProgress.Materials.All(x => x.IsPassed == true);

                if (allMaterialsPassed)
                {
                    //if all materials passed => add skills for user and add course to Passed list, delete course from processing list
                    WorkAfterSuccessfullyPassingCourse(allMaterialsPassed, courseVMInProgress);
                }
                else
                {
                    //update passed materials in course
                    userService.UpdateCourseInProgress(courseToProgressFromProcessList.Id,
                      Mapping.Mapping.CreateListMapFromVMToDomainWithIncludeMaterialType<MaterialViewModel, Material, VideoViewModel, Video, ArticleViewModel, Article, BookViewModel, Book>(courseVMInProgress.Materials));
                }
                
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
        }
        
        public void StartPassCourse()
        {
            //Get all courses
            List<CourseViewModel> coursesListVM = GetListOfCoursesFromServiceAfterMappingToVM(userService.GetAvailableCoursesForUser());

            if (coursesListVM.Count <= 0)
            {
                ShowMessageAndReturnToMainMenu("No courses available");
            }

            //Show all courses
            ShowCoursesToPass(coursesListVM);

            //Get course id from user to passing
            int courseIdToPass = GetIdFromUserToPassCourse();
            //Check, may be we this course course already started
            if (userService.GetListWithCoursesInProgress().Any(x => x.Id == courseIdToPass))
            {
                ShowMessageAndReturnToMainMenu("You have already started this course");
            }

            if(!coursesListVM.Any(x => x.Id == courseIdToPass))
            {
                ShowMessageAndReturnToMainMenu("Сourses with this id do not exist");
            }

            //Greate course to continue passing
            CourseViewModel courseInProgress = coursesListVM.Where(x => x.Id == courseIdToPass).FirstOrDefault();

            if(courseInProgress != null)
            {
                Console.Clear();
                //add course to Progress List
                userService.AddCourseInProgress(courseInProgress.Id);
                PassCourseConsoleMessageHelper.ShowInfoToStartPassingCourse();
                //passing the course
                PassingCourse(ref courseInProgress, courseInProgress.Materials.ToList());
                //checking if all materials have been worked out
                bool allMaterialsPassed = courseInProgress.Materials.All(x => x.IsPassed == true);
                //add skills for user and add course to Passed list
                WorkAfterSuccessfullyPassingCourse(allMaterialsPassed, courseInProgress);
                ProgramBranch.SelectFirstStepForAuthorizedUser();
            }
        }

        List<CourseViewModel> GetListOfCoursesFromServiceAfterMappingToVM(List<Course> courses)
        {
            //Mapping entity to view model, and mapping include entities lists
            List<CourseViewModel> coursesListVM = Mapping.Mapping.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(courses);

            foreach (var course in coursesListVM)
            {
                course.Materials = Mapping.Mapping.CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(courseService.GetMaterialsFromCourse(course.Id));
                course.Skills = Mapping.Mapping.CreateListMap<Skill, SkillViewModel>(courseService.GetSkillsFromCourse(course.Id));
            }

            return coursesListVM;
        }


        void WorkAfterSuccessfullyPassingCourse(bool allMaterialsPassed, CourseViewModel successfullyСompletedСourse)
        {
            if (allMaterialsPassed)
            {
                //add course to Passed List
                userService.AddCourseToPassed(successfullyСompletedСourse.Id);
                //delete course from Progress List
                userService.DeleteCourseFromProgress(successfullyСompletedСourse.Id);

                //add skills to user
                foreach (var skill in successfullyСompletedСourse.Skills)
                {
                    userService.AddSkill(Mapping.Mapping.CreateMapFromVMToDomain<SkillViewModel, Skill>(skill));
                }

                Console.WriteLine($"Congratulations, you have successfully completed the course {successfullyСompletedСourse.Name}");
                Thread.Sleep(4000);
            }
        }
        void PassingCourse(ref CourseViewModel courseInProgress, List<MaterialViewModel> materials)
        {
            foreach (var material in materials)
            {
                //Show info about material
                Console.WriteLine(material.ToString());
                Console.Write("\nHas the material been studied (Enter '+') or exit from studying course (Enter 'Exit')?");
                //get value from user
                string learnMaterial = Console.ReadLine();

                if (learnMaterial == "+")
                {
                    //select true to passed material
                    courseInProgress.Materials.Select(x => x).Where(x => x.Name == material.Name).FirstOrDefault().IsPassed = true;
                    //update user with new value
                    userService.UpdateValueOfPassMaterialInProgress(courseInProgress.Id, material.Id);
                }
                else if (learnMaterial.ToLower() == "exit")
                {
                    Console.WriteLine("Сourse not finished!");
                    break;
                }
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
                //show course name
                Console.WriteLine($"\n№ {course.Id} - {course.Name}");
                Console.WriteLine("This course includes such materials:");

                //Show info about all materials
                foreach (var material in course.Materials)
                {
                    Console.WriteLine(material.ToString());
                }

                Console.WriteLine("After completing this course, you will receive such skills: \n");

                //Show all skills in course
                foreach (var skill in course.Skills)
                {
                    Console.WriteLine($"{skill.Name}");
                }
            }
        }

        void ShowMessageAndReturnToMainMenu(string message)
        {
            //Show message with error and return to main menu
            Console.WriteLine(message);
            Thread.Sleep(4000);
            ProgramBranch.SelectFirstStepForAuthorizedUser();
        }
    }
}
