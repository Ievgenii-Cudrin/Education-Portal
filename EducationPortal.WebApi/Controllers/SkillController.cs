using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPartal.WebApi.ModelsView;
using EducationPortal.BLL.Interfaces;
using EducationPortal.WebApi.ModelsView;
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
    public class SkillController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly ISkillService skillService;
        private readonly ICourseService courseService;
        private readonly ICourseSkillService courseSkillService;
        private readonly ILogger<SkillController> logger;
        private IOperationResult operationResult;

        public SkillController(
            IMapper mapper,
            IUserService userService,
            ISkillService skillService,
            ICourseService courseService,
            ICourseSkillService courseSkillService,
            IOperationResult operationResult,
            ILogger<SkillController> logger)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.skillService = skillService;
            this.courseService = courseService;
            this.courseSkillService = courseSkillService;
            this.operationResult = operationResult;
            this.logger = logger;
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
                var recordsFromDbForOnePage = await this.skillService.GetAllSkillsForOnePage(coursesSkip, pageSize);
                var coursesVM = this.mapper.Map<IEnumerable<Skill>, IEnumerable<SkillViewModel>>(recordsFromDbForOnePage);

                return Ok(coursesVM);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> Create([FromBody] SkillViewModel skillViewModel)
        {
            try
            {
                if (skillViewModel == null)
                {
                    return BadRequest();
                }

                Skill skillDomain = this.mapper.Map<Skill>(skillViewModel);

                //Operation result
                this.operationResult = await this.skillService.CreateSkill(skillDomain);

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

        [HttpPost("AddSkillToCourse")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> AddSkillToCourse([FromBody] SkillViewModel skillVM)
        {
            try
            {
                bool skillExist = await this.skillService.ExistSkill(skillVM.Id);

                if (!skillExist)
                {
                    return BadRequest();
                }

                int courseId = await this.courseService.GetLastId();
                this.operationResult = await this.courseSkillService.AddSkillToCourse(courseId, skillVM.Id);

                if (!this.operationResult.IsSucceed)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> Delete([FromBody] SkillViewModel skillVM)
        {
            try
            {
                var skill = await this.skillService.GetSkill(skillVM.Id);

                if (skill == null)
                {
                    return BadRequest();
                }

                await this.skillService.Delete(skill.Id);

                return Ok();
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
        public async Task<IActionResult> Update([FromRoute][Range(1, int.MaxValue)][Required] int id, [FromBody] SkillViewModel skillVM)
        {
            try
            {
                var skill = await this.skillService.GetSkill(id);

                if (skill == null)
                {
                    return NotFound();
                }

                skill.Name = skillVM.Name;

                await this.skillService.UpdateSkill(skill);

                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
