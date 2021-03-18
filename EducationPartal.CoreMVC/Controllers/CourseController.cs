using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPartal.CoreMVC.Interfaces;
using EducationPartal.CoreMVC.ModelsView;
using EducationPortal.BLL.Interfaces;
using EducationPortal.CoreMVC.Interfaces;
using EducationPortal.Domain.Entities;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.CoreMVC.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly IAutoMapperService mapperService;
        private readonly ICourseService courseService;
        private readonly IAuthorizedUser authorizedUser;
        private readonly IUserCourseSqlService userCourseSqlService;
        private IOperationResult operationResult;

        public CourseController(
            IAutoMapperService autoMapperService,
            ICourseService courseService,
            IAuthorizedUser authorizedUser,
            IOperationResult operationResult,
            IUserCourseSqlService userCourseSqlService)
        {
            this.authorizedUser = authorizedUser;
            this.mapperService = autoMapperService;
            this.courseService = courseService;
            this.operationResult = operationResult;
            this.userCourseSqlService = userCourseSqlService;
        }

        // GET: CourseController
        public async Task<ActionResult> Index(int id = 1)
        {
            const int pageSize = 3;

            if (id < 1)
            {
                id = 1;
            }

            int coursesCount = await this.courseService.GetCount();
            var pager = new PageInfo(coursesCount, id, pageSize);
            int coursesSkip = (id - 1) * pageSize;

            var recordsFromDbForOnePage = await this.courseService.GetCoursesPerPage(coursesSkip, pager.PageSize);

            List<CourseViewModel> coursesVM = this.mapperService.
                CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(recordsFromDbForOnePage.ToList());
            this.ViewBag.Pager = pager;

            return View(coursesVM);
        }

        public async Task<ActionResult> ShowAllPAssedCourse()
        {
            var allPassedCourse = await this.userCourseSqlService.GetAllPassedCourse(this.authorizedUser.User.Id);
            List<CourseViewModel> coursesVM = this.mapperService.
                CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(allPassedCourse.ToList());

            return View(coursesVM);
        }

        // GET: CourseController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                Course courseDomain = this.mapperService.CreateMapFromVMToDomain<CourseViewModel, Course>(courseViewModel);

                //Operation result
                this.operationResult = await this.courseService.CreateCourse(courseDomain);

                if (this.operationResult.IsSucceed)
                {
                    return RedirectToAction("ContinueCourseCreating");
                }
                else
                {
                    ViewData["Message"] = this.operationResult.Message;
                }
            }
            return View();
        }

        public async Task<ActionResult> ContinueCourseCreating()
        {
            try
            {
                int courseId = await this.courseService.GetLastId();
                var courseDomain = await this.courseService.GetCourse(courseId);
                CourseViewModel courseViewModel = this.mapperService.CreateMapFromVMToDomain<Course, CourseViewModel>(courseDomain);

                var materialsInCourseDomain = await this.courseService.GetMaterialsFromCourse(courseId);
                var skillsInCourseDomain = await this.courseService.GetSkillsFromCourse(courseId);

                courseViewModel.Materials = this.mapperService.
                    CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(materialsInCourseDomain.ToList());
                courseViewModel.Skills = this.mapperService.CreateListMap<Skill, SkillViewModel>(skillsInCourseDomain.ToList());

                return View(courseViewModel);
            }
            catch
            {

            }
            return View();
        }

        // GET: CourseController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var courseViewModel = this.mapperService.CreateMapFromVMToDomain<Course, CourseViewModel>(await this.courseService.GetCourse(id));
            return View(courseViewModel);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CourseViewModel courseVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Course courseDomain = this.mapperService.CreateMapFromVMToDomain<CourseViewModel, Course>(courseVM);
                    this.operationResult = await this.courseService.UpdateCourse(courseDomain);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseController/Delete/5
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
