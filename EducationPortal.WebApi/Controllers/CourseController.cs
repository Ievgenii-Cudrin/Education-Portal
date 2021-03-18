using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPartal.WebApi.ModelsView;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICourseService courseService;
        private readonly IUserCourseSqlService userCourseSqlService;
        private readonly ILogger<CourseController> logger;
        private readonly ILogInService logInService;
        private readonly IAuthorizedUser authorizedUser;
        private IOperationResult operationResult;

        public CourseController(
            IMapper mapper,
            ICourseService courseService,
            IOperationResult operationResult,
            IUserCourseSqlService userCourseSqlService,
            IAuthorizedUser authorizedUser,
            ILogger<CourseController> logger,
            ILogInService logInService)
        {
            this.mapper = mapper;
            this.courseService = courseService;
            this.operationResult = operationResult;
            this.userCourseSqlService = userCourseSqlService;
            this.logger = logger;
            this.authorizedUser = authorizedUser;
            this.logInService = logInService;
        }

        [HttpPost]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> Create([FromBody] CourseViewModel courseViewModel)
        {
            try
            {
                if (courseViewModel == null)
                {
                    return BadRequest();
                }

                Course courseDomain = this.mapper.Map<Course>(courseViewModel);

                //Operation result
                this.operationResult = await this.courseService.CreateCourse(courseDomain);

                if (this.operationResult.IsSucceed == true)
                {
                    return Ok();
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

        [HttpGet("{id:int}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> Get([FromRoute][Range(1, int.MaxValue)][Required] int id)
        {
            try
            {
                int pageSize = 3;
                int coursesSkip = (id - 1) * pageSize;
                var recordsFromDbForOnePage = await this.courseService.GetCoursesPerPage(coursesSkip, pageSize);
                var coursesVM = this.mapper.Map<IEnumerable<Course>, IEnumerable<CourseViewModel>>(recordsFromDbForOnePage);

                return Ok(coursesVM);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("GetAllPassedCourse")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var allPassedCourse = await this.userCourseSqlService.GetAllPassedCourse(this.authorizedUser.User.Id);
                var coursesVM = this.mapper.Map<IEnumerable<Course>, IEnumerable<CourseViewModel>>(allPassedCourse);

                return Ok(coursesVM);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Update([FromRoute][Range(1, int.MaxValue)][Required] int id, [FromBody] CourseViewModel courseVM)
        {
            try
            {
                var course = await this.courseService.GetCourse(id);

                if (course == null)
                {
                    return NotFound();
                }

                course.Name = courseVM.Name;
                course.Description = courseVM.Description;
                this.operationResult = await this.courseService.UpdateCourse(course);

                if (this.operationResult.IsSucceed)
                {
                    return Ok();
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
    }
}
