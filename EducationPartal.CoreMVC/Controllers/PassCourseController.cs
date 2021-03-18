using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPartal.CoreMVC.Interfaces;
using EducationPartal.CoreMVC.ModelsView;
using EducationPortal.BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.CoreMVC.Controllers
{
    [Authorize]
    public class PassCourseController : Controller
    {
        private readonly IUserCourseSqlService userCourseService;
        private readonly IUserService userService;
        private readonly IAuthorizedUser authorizedUser;
        private readonly ICourseService courseService;
        private readonly IUserMaterialSqlService userMaterialSqlService;
        private readonly IAutoMapperService autoMapperService;

        public PassCourseController(
            IUserCourseSqlService userCourseService,
            IUserService userService,
            IAuthorizedUser authorizedUser,
            ICourseService courseService,
            IUserMaterialSqlService userMaterialSqlService,
            IAutoMapperService autoMapperService)
        {
            this.userCourseService = userCourseService;
            this.userService = userService;
            this.authorizedUser = authorizedUser;
            this.courseService = courseService;
            this.userMaterialSqlService = userMaterialSqlService;
            this.autoMapperService = autoMapperService;
        }

        private const string successfullCoursePass = "Course successfully passed!";
        private const string courseNotPassed = "Not all materials pass in course";
        private const string courseSuccessfullyPassed = "Course successfully pased";

        // GET: PassCourseController
        public async Task<ActionResult> Index(int id)
        {
            var userCourseDomain = await this.userCourseService.GetUserCourse(this.authorizedUser.User.Id, id);

            bool courseIsPassed = userCourseDomain == null ? false : userCourseDomain.IsPassed == true ? true : false;

            if (courseIsPassed)
            {
                ViewData["Message"] = courseSuccessfullyPassed;
                return View("Result");
            }

            //get course and map
            var courseToPassDomain = await this.courseService.GetCourse(id);
            var courseToPassVm = this.autoMapperService.CreateMapFromVMToDomainWithIncludeLsitType
                <Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(courseToPassDomain);

            //get not passed materials and map
            var materialsInCourseDomain = await this.userService.GetAllNotPassedMaterialsInCourse(id);
            var materialsVM = this.autoMapperService.CreateListMapFromVMToDomainWithIncludeMaterialType
                <Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(materialsInCourseDomain.ToList());

            courseToPassVm.Materials = materialsVM.Where(x => x.IsPassed == false).ToList();

            bool existUserCourse = await this.userCourseService.ExistUserCourseByUserIdCourseId(this.authorizedUser.User.Id, id);

            if (!existUserCourse)
            {
                await this.userService.AddCourseInProgress(id);
            }

            TempData["courseId"] = id;
            return View(courseToPassVm.Materials);
        }

        public async Task<ActionResult> PassMaterials(List<MaterialViewModel> materials)
        {
            int courseId = (int)TempData["courseId"];

            foreach (var material in materials)
            {
                if (material.IsPassed == true)
                {
                    await this.userService.UpdateValueOfPassMaterialInProgress(courseId, material.Id);
                }
            }

            if (materials.All(x => x.IsPassed == true))
            {
                //Add course to passed
                await this.userService.AddCourseToPassed(courseId);
                var courseSkill = await this.courseService.GetSkillsFromCourse(courseId);

                //Add skills to user
                await this.userService.AddSkills(courseSkill.ToList());

                //show message
                ViewData["Message"] = successfullCoursePass;
            }
            else
            {
                ViewData["Message"] = courseNotPassed;
            }

            return View("Result");
        }

        public async Task<ActionResult> PassNotFinishedCourse()
        {
            var notPassedCourse = await this.userCourseService.GetAllCourseInProgress(this.authorizedUser.User.Id);
            
            List<CourseViewModel> coursesVM = this.autoMapperService.
                CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(notPassedCourse.ToList());

            return View(coursesVM);
        }

        public async Task<ActionResult> ContinuePassingCourse(int id)
        {
            return null;
        }

        // GET: PassCourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PassCourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PassCourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PassCourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PassCourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PassCourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PassCourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
