using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPartal.CoreMVC.Interfaces;
using EducationPartal.CoreMVC.ModelsView;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.CoreMVC.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        private readonly IAutoMapperService autoMapperService;
        private readonly IUserService userService;
        private readonly ISkillService skillService;
        private readonly ICourseService courseService;
        private readonly ICourseSkillService courseSkillService;
        private IOperationResult operationResult;

        public SkillController(
            IAutoMapperService autoMapperService,
            IUserService userService,
            ISkillService skillService,
            ICourseService courseService,
            ICourseSkillService courseSkillService,
            IOperationResult operationResult)
        {
            this.autoMapperService = autoMapperService;
            this.userService = userService;
            this.skillService = skillService;
            this.courseService = courseService;
            this.courseSkillService = courseSkillService;
            this.operationResult = operationResult;
        }
        // GET: SkillController
        public async Task<ActionResult> Index(int id = 1)
        {
            const int pageSize = 3;

            if (id < 1)
            {
                id = 1;
            }

            int skillsCount = await this.skillService.GetCount();
            var pager = new PageInfo(skillsCount, id, pageSize);
            int skillsSkip = (id - 1) * pageSize;

            var skillsFromDbForOnePage = await this.skillService.GetAllSkillsForOnePage(skillsSkip, pager.PageSize);

            var skillsViewModel = this.autoMapperService.CreateListMap<Skill, SkillViewModel>(skillsFromDbForOnePage.ToList());
            this.ViewBag.Pager = pager;

            return View(skillsViewModel);
        }

        // GET: SkillController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SkillViewModel skillVM)
        {
            try
            {
                var skillDomain = this.autoMapperService.CreateMapFromVMToDomain<SkillViewModel, Skill>(skillVM);
                await this.skillService.CreateSkill(skillDomain);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> AddSkillToCourse(int id)
        {
            int courseId = await this.courseService.GetLastId();
            this.operationResult = await this.courseSkillService.AddSkillToCourse(courseId, id);

            if (this.operationResult.IsSucceed)
            {
                return RedirectToAction("ContinueCourseCreating", "Course");
            }
            else
            {
                ViewData["Message"] = this.operationResult.Message;
                return View();
            }
        }

        public async Task<ActionResult> DeleteSkillFromCourse(int id)
        {
            int courseId = await this.courseService.GetLastId();
            this.operationResult = await this.courseSkillService.DeleteSkillFromCourse(courseId, id);

            if (this.operationResult.IsSucceed)
            {
                return RedirectToAction("ContinueCourseCreating", "Course");
            }
            else
            {
                ViewData["Message"] = this.operationResult.Message;
                return View();
            }
        }

        // GET: SkillController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var skillDomain = await this.skillService.GetSkill(id);
            var skillVM = this.autoMapperService.CreateMapFromVMToDomain<Skill, SkillViewModel>(skillDomain);
            return View(skillVM);
        }

        // POST: SkillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SkillViewModel skillViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var skillDomain = this.autoMapperService.CreateMapFromVMToDomain<SkillViewModel, Skill>(skillViewModel);
                    await this.skillService.UpdateSkill(skillDomain);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
