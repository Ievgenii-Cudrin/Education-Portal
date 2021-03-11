using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.ServicesSql;
using EducationPortal.Domain.Entities;
using EducationPortal.PL.Helpers;
using EducationPortal.PL.Interfaces;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Interfaces;
using Entities;

namespace EducationPortal.PL.Controller
{
    public class PassCourseController : IPassCourseController
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly IMaterialController materialController;
        private readonly IMapperService mapperService;
        private readonly IMaterialService materialService;
        private readonly IUserCourseSqlService userCourseService;
        private IApplication application;

        public PassCourseController(
            ICourseService courseService,
            IUserService userService,
            IMaterialController materialController,
            IMapperService mapper,
            IMaterialService materialService,
            IUserCourseSqlService userCourseService)
        {
            this.courseService = courseService;
            this.userService = userService;
            this.materialController = materialController;
            this.mapperService = mapper;
            this.materialService = materialService;
            this.userCourseService = userCourseService;
        }

        public void WithApplication(IApplication application)
        {
            this.application = application;
        }

        public async Task<int> GetIdFromUserToPassCourse()
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
                await this.application.SelectFirstStepForAuthorizedUser();
            }

            return -1;
        }

        public async Task StartPassingCourseFromProgressList()
        {
            var courseDomainListInProgress = await this.userService.GetListWithCoursesInProgress();

            // Get all courses in progress
            List<CourseViewModel> coursesListInProgressVM = await this.GetListOfCoursesFromServiceAfterMappingToVM((List<Course>)courseDomainListInProgress);

            // CHeck, we have courses in progress, or no
            if (coursesListInProgressVM.Count <= 0)
            {
                await this.ShowMessageAndReturnToMainMenu("You have no unfinished courses!");
            }

            // Show all courses in progress
            this.ShowCoursesToPass(coursesListInProgressVM);

            // get course id from user to continue
            int courseInProgressIdToPass = await this.GetIdFromUserToPassCourse();

            // checking valid course id
            if (!coursesListInProgressVM.Any(x => x.Id == courseInProgressIdToPass))
            {
                await this.ShowMessageAndReturnToMainMenu("Invalid ID.");
            }

            // get course in progress
            List<Course> courseToProgressFromProcessList = (List<Course>)await this.userService.GetListWithCoursesInProgress();

            // mapping to course domain to course view model
            CourseViewModel courseVMInProgress = this.mapperService.CreateMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(courseToProgressFromProcessList.Where(x => x.Id == courseInProgressIdToPass).FirstOrDefault());

            // mapping materials in progogress
            courseVMInProgress.Materials = this.materialController.GetAllMaterialVMAfterMappingFromMaterialDomain((List<Material>)await this.userService.GetMaterialsFromCourseInProgress(courseVMInProgress.Id));

            // mapping skills
            courseVMInProgress.Skills = this.mapperService.CreateListMap<Skill, SkillViewModel>((List<Skill>)await this.userService.GetSkillsFromCourseInProgress(courseVMInProgress.Id));

            if (courseVMInProgress != null)
            {
                Console.Clear();

                // List<MaterialViewModel> materials = materialController.GetAllMaterialVMAfterMappingFromMaterialDomain(courseToProgressFromProcessList.Materials.Select(x => x).Where(x => x.IsPassed == false).ToList());
                List<MaterialViewModel> materials = courseVMInProgress.Materials.Where(x => x.IsPassed == false).ToList();

                // Add to method course from processing list, and materials from this course, which do not passed
                await this.PassingCourse(courseVMInProgress, materials);

                // checking if all materials have been worked out
                bool allMaterialsPassed = courseVMInProgress.Materials.All(x => x.IsPassed == true);

                if (allMaterialsPassed)
                {
                    // if all materials passed => add skills for user and add course to Passed list, delete course from processing list
                    await this.WorkAfterSuccessfullyPassingCourse(allMaterialsPassed, courseVMInProgress);
                }
                else
                {
                    Console.WriteLine($"You do not passed all materials in course. Please, continue later.");
                    Thread.Sleep(4000);
                }

                await this.application.SelectFirstStepForAuthorizedUser();
            }
        }

        public async Task StartPassCourse()
        {
            int numberOfPage = 1;
            bool selectedPage = false;
            List<CourseViewModel> coursesListVM;

            do
            {
                Console.Clear();
                const int pageSize = 3;
                int recordsCount = await this.courseService.GetCount();
                var pager = new PageInfo(recordsCount, numberOfPage, pageSize);
                int coursesSkip = (numberOfPage - 1) * pageSize;
                var coursesOnPage = await this.courseService.GetCoursesPerPage(coursesSkip, pager.PageSize);
                coursesListVM = await this.GetListOfCoursesFromServiceAfterMappingToVM(coursesOnPage.ToList());

                // ShowCourses
                this.ShowCoursesToPass(coursesListVM);

                Console.WriteLine($"Count of pages - {pager.TotalPages}");
                Console.WriteLine($"Current page - {numberOfPage}");
                Console.WriteLine($"Do you want select another PAGE (enter page) or add COURSE to pass (enter course) from this page?");

                string userChoice = Console.ReadLine();

                switch (userChoice.ToLower())
                {
                    case "page":
                        selectedPage = true;
                        Console.WriteLine($"Enter page number: ");
                        numberOfPage = int.Parse(Console.ReadLine());
                        break;
                    case "course":
                        selectedPage = false;
                        break;
                    default:
                        numberOfPage = 1;
                        selectedPage = true;
                        break;
                }
            }
            while (selectedPage);

            // Get course id from user to passing
            int courseIdToPass = await this.GetIdFromUserToPassCourse();

            // Check, may be we this course course already started
            if (await this.userCourseService.CourseWasStarted(courseIdToPass))
            {
                await this.ShowMessageAndReturnToMainMenu("You have already started this course");
            }

            if (!coursesListVM.Any(x => x.Id == courseIdToPass))
            {
                await this.ShowMessageAndReturnToMainMenu("Сourses with this id do not exist");
            }

            // Greate course to continue passing
            CourseViewModel courseInProgress = coursesListVM.Where(x => x.Id == courseIdToPass).FirstOrDefault();

            var allNotPassedMaterialsDomain = await this.userService.GetAllNotPassedMaterialsInCourse(courseIdToPass);
            var allNodPassedAfterMpping = this.mapperService.CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>((List<Material>)allNotPassedMaterialsDomain);
            courseInProgress.Materials = allNodPassedAfterMpping;

            if (courseInProgress != null)
            {
                Console.Clear();

                // add course to Progress List
                await this.userService.AddCourseInProgress(courseInProgress.Id);
                PassCourseConsoleMessageHelper.ShowInfoToStartPassingCourse();

                // passing the course
                await this.PassingCourse(courseInProgress, courseInProgress.Materials.ToList());

                // checking if all materials have been worked out
                bool allMaterialsPassed = courseInProgress.Materials.All(x => x.IsPassed == true);

                // add skills for user and add course to Passed list
                await this.WorkAfterSuccessfullyPassingCourse(allMaterialsPassed, courseInProgress);
                await this.application.SelectFirstStepForAuthorizedUser();
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

        private async Task ShowMessageAndReturnToMainMenu(string message)
        {
            // Show message with error and return to main menu
            Console.WriteLine(message);
            Thread.Sleep(4000);
            await this.application.SelectFirstStepForAuthorizedUser();
        }

        private async Task PassingCourse(CourseViewModel courseInProgress, List<MaterialViewModel> materials)
        {
            foreach (var material in materials)
            {
                if (material.IsPassed == true)
                {
                    await this.userService.UpdateValueOfPassMaterialInProgress(courseInProgress.Id, material.Id);
                }
                else
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
                        await this.userService.UpdateValueOfPassMaterialInProgress(courseInProgress.Id, material.Id);
                    }
                    else if (learnMaterial.ToLower() == "exit")
                    {
                        Console.WriteLine("Сourse not finished!");
                        break;
                    }
                }
            }
        }

        private async Task<List<CourseViewModel>> GetListOfCoursesFromServiceAfterMappingToVM(List<Course> courses)
        {
            // Mapping entity to view model, and mapping include entities lists
            List<CourseViewModel> coursesListVM = this.mapperService.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(courses);

            foreach (var course in coursesListVM)
            {
                course.Materials = this.mapperService.CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>((List<Material>)await this.courseService.GetMaterialsFromCourse(course.Id));
                course.Skills = this.mapperService.CreateListMap<Skill, SkillViewModel>((List<Skill>)await this.courseService.GetSkillsFromCourse(course.Id));
            }

            return coursesListVM;
        }

        private async Task WorkAfterSuccessfullyPassingCourse(bool allMaterialsPassed, CourseViewModel successfullyСompletedСourse)
        {
            if (allMaterialsPassed)
            {
                // add course to Passed List
                await this.userService.AddCourseToPassed(successfullyСompletedСourse.Id);

                // add skills to user
                foreach (var skill in successfullyСompletedСourse.Skills)
                {
                    await this.userService.AddSkill(this.mapperService.CreateMapFromVMToDomain<SkillViewModel, Skill>(skill));
                }

                Console.WriteLine($"Congratulations, you have successfully completed the course {successfullyСompletedСourse.Name}");
                Thread.Sleep(4000);
            }
        }
    }
}
