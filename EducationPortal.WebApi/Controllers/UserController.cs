using AutoMapper;
using DataAccessLayer.Entities;
using EducationPartal.WebApi.ModelsView;
using EducationPortal.BLL.DTO;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using EducationPortal.WebApi.ModelsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCourseSqlService userCourseService;
        private readonly IUserSkillSqlService userSkillSqlService;
        private readonly IAuthorizedUser authorizedUser;
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;

        public UserController(
            IUserCourseSqlService userCourseService,
            IUserSkillSqlService userSkillSqlService,
            IAuthorizedUser authorizedUser,
            IMapper mapper,
            ILogger<UserController> logger)
        {
            this.userCourseService = userCourseService;
            this.userSkillSqlService = userSkillSqlService;
            this.authorizedUser = authorizedUser;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet("UserInfo")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> GetUserInfo()
        {
            try
            {
                if (this.authorizedUser.User != null)
                {
                    return Ok(this.authorizedUser.User);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("CourseInProgress")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> CourseInProgress()
        {
            try
            {
                var courseInProgress = await this.userCourseService.AllNotPassedCourseWithCompletedPercent(this.authorizedUser.User.Id);
                var courseVM = this.mapper.Map<IEnumerable<CourseDTO>, IEnumerable<CourseViewModel>>(courseInProgress);
                return Ok(courseVM);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("ShowUserSkills")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> ShowUserSkills()
        {
            try
            {
                var skills = await this.userSkillSqlService.GetAllUSerSkillsWithInclude(this.authorizedUser.User.Id);
                var skillWithCountViewModel = this.mapper.Map<IEnumerable<UserSkill>, IEnumerable<SkillWithCountViewModel>>(skills);

                return Ok(skillWithCountViewModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
