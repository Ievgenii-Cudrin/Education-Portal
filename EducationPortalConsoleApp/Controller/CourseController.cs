using System;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.BLL.Interfaces;
using EducationPortal.PL.InstanceCreator;
using EducationPortal.PL.Interfaces;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Interfaces;
using Entities;

namespace EducationPortalConsoleApp.Controller
{
    public class CourseController : ICourseController
    {
        private readonly ICourseService courseService;
        private readonly IMapperService mapperService;
        private readonly ISkillController skillController;
        private IOperationResult operationResult;
        private IApplication application;

        public CourseController(
            ICourseService courseService,
            IMapperService mapper,
            ISkillController skilCntrl,
            IOperationResult operationResult)
        {
            this.courseService = courseService;
            this.mapperService = mapper;
            this.skillController = skilCntrl;
            this.operationResult = operationResult;
        }

        public void WithApplication(IApplication application)
        {
            this.application = application;
        }

        public async Task CreateNewCourse()
        {
            Console.Clear();

            // Create course
            CourseViewModel courseVM = CourseVMInstanceCreator.CreateCourse();

            // mapping to Domain model
            var courseDomain = this.mapperService.CreateMapFromVMToDomain<CourseViewModel, Course>(courseVM);
            this.operationResult = await this.courseService.CreateCourse(courseDomain);

            if (this.operationResult.IsSucceed)
            {
                // Add materials to course
                await this.AddMaterialToCourse(courseDomain.Id);

                // Add skills to course
                await this.AddSkillsToCourse(courseDomain.Id);

                // go ro start menu
                await this.application.SelectFirstStepForAuthorizedUser();
            }
            else
            {
                Console.WriteLine(this.operationResult.Message);

                // Go to start menu
                await this.application.SelectFirstStepForAuthorizedUser();
            }
        }

        private async Task AddMaterialToCourse(int courseId)
        {
            string userChoice;
            do
            {
                Material materialDomain = await this.application.SelectMaterialForAddToCourse(courseId);
                this.operationResult = await this.courseService.AddMaterialToCourse(courseId, materialDomain);

                // check, material exist in course, or no
                if (!this.operationResult.IsSucceed)
                {
                    Console.WriteLine(this.operationResult.Message);
                }

                Console.WriteLine("Do you want to add more material (Enter YES)?");
                userChoice = Console.ReadLine();
            }
            while (userChoice.ToLower() == "yes");
        }

        private async Task AddSkillsToCourse(int courseId)
        {
            string userChoice;

            do
            {
                Console.WriteLine("Add skill to your course: ");

                // create skill
                var skillDomain = await this.skillController.CreateSkill();

                // add skill to course after mapping
                await this.courseService.AddSkillToCourse(courseId, skillDomain);
                Console.WriteLine("Do you want to add one more skill (Enter YES)?");
                userChoice = Console.ReadLine();
            }
            while (userChoice.ToLower() == "yes");
        }
    }
}
