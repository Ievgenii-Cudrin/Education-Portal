namespace EducationPortalConsoleApp.Controller
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using EducationPortal.PL.InstanceCreator;
    using EducationPortal.PL.Interfaces;
    using EducationPortal.PL.Mapping;
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Interfaces;
    using Entities;

    public class CourseController : ICourseController
    {
        private ICourseService courseService;
        private IMapperService mapperService;
        private ISkillController skillController;
        private IApplication application;

        public CourseController(
            ICourseService courseService,
            IMapperService mapper,
            ISkillController skilCntrl)
        {
            this.courseService = courseService;
            this.mapperService = mapper;
            this.skillController = skilCntrl;
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
            var course = this.mapperService.CreateMapFromVMToDomain<CourseViewModel, Course>(courseVM);
            // mapping to Domain model
            var courseDomain = this.mapperService.CreateMapFromVMToDomain<CourseViewModel, Course>(courseVM);
            bool success = await this.courseService.CreateCourse(courseDomain);

            if (success)
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
                Console.WriteLine("Course exist");

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
                bool successOperation = await this.courseService.AddMaterialToCourse(courseId, materialDomain);

                // check, material exist in course, or no
                if (!successOperation)
                {
                    Console.WriteLine("Material exist in course");
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
