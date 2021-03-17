using DataAccessLayer.Entities;
using EducationPartal.CoreMVC.Interfaces;
using EducationPartal.CoreMVC.ModelsView;
using EducationPortal.BLL.DTO;
using EducationPortal.BLL.Interfaces;
using EducationPortal.CoreMVC.ModelsView;
using EducationPortal.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.CoreMVC.Controllers
{
    [Authorize]

    public class UserController : Controller
    {
        private readonly IUserCourseSqlService userCourseService;
        private readonly IUserSkillSqlService userSkillSqlService;
        private readonly IAuthorizedUser authorizedUser;
        private readonly IAutoMapperService autoMapperService;

        public UserController(
            IUserCourseSqlService userCourseService,
            IUserSkillSqlService userSkillSqlService,
            IAuthorizedUser authorizedUser,
            IAutoMapperService autoMapperService)
        {
            this.userCourseService = userCourseService;
            this.userSkillSqlService = userSkillSqlService;
            this.authorizedUser = authorizedUser;
            this.autoMapperService = autoMapperService;
        }
        // GET: UserController
        public async Task<ActionResult> CourseInProgress()
        {
            var courseInProgress = await this.userCourseService.AllNotPassedCourseWithCompletedPercent(this.authorizedUser.User.Id);
            var courseVM = this.autoMapperService.CreateListMap
                <CourseDTO, CourseViewModel>(courseInProgress.ToList());

            return View(courseVM);
        }

        public async Task<ActionResult> ShowUserSkills()
        {
            //get skills 
            var skills = await this.userSkillSqlService.GetAllUSerSkillsWithInclude(this.authorizedUser.User.Id);
            var skillWithCountViewModel = this.autoMapperService.CreateSkillListMapFromVMToDomainWithIncludeSkillType
                <UserSkill, SkillWithCountViewModel, Skill, SkillViewModel>(skills.ToList());

            return View(skillWithCountViewModel);
        }

        public async Task<ActionResult> ShowUserInfo()
        {
            var user = this.authorizedUser.User;
            var userVM = this.autoMapperService.CreateMapFromVMToDomain<User, UserViewModel>(user);

            return View(userVM);
        }
    }
}
