using AutoMapper;
using BusinessLogicLayer.Interfaces;
using EducationPartal.WebApi.ModelsView;
using EducationPortal.BLL.Interfaces;
using Entities;
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
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService materialService;
        private readonly IAuthorizedUser authorizedUser;
        private readonly IMapper mapper;
        private readonly ICourseMaterialService courseMaterialService;
        private readonly ICourseService courseService;
        private readonly ILogger<MaterialController> logger;
        private IOperationResult operationResult;

        public MaterialController(
            IMaterialService materialService,
            IAuthorizedUser authorizedUser,
            IMapper mapper,
            ICourseMaterialService courseMaterialService,
            ICourseService courseService,
            IOperationResult operationResult,
            ILogger<MaterialController> logger)
        {
            this.materialService = materialService;
            this.authorizedUser = authorizedUser;
            this.mapper = mapper;
            this.courseMaterialService = courseMaterialService;
            this.courseService = courseService;
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
                int materialsSkip = (id - 1) * pageSize;
                var materialsFromDbForOnePage = await this.materialService.GetAllMaterialsForOnePage(materialsSkip, pageSize);
                var materialsVM = this.mapper.Map<IEnumerable<Material>, IEnumerable<MaterialViewModel>>(materialsFromDbForOnePage);

                return Ok(materialsVM);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("AddMaterialToCourse")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> Create([FromBody] MaterialViewModel materialVM)
        {
            try
            {
                var material = await this.materialService.GetMaterial(materialVM.Id);

                if(material == null)
                {
                    return BadRequest();
                }

                var courseId = await this.courseService.GetLastId();
                this.operationResult = await this.courseMaterialService.AddMaterialToCourse(courseId, material.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("DeleteMaterialFromCourse")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> Delete([FromBody] MaterialViewModel materialVM)
        {
            try
            {
                var material = await this.materialService.GetMaterial(materialVM.Id);

                if (material == null)
                {
                    return BadRequest();
                }

                var courseId = await this.courseService.GetLastId();
                this.operationResult = await this.courseMaterialService.DeleteMaterialFromCourse(courseId, material.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("CreateVideo")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> Create([FromBody] VideoViewModel materialVM)
        {
            try
            {
                var material = await this.materialService.GetMaterial(materialVM.Id);

                if (material != null)
                {
                    return BadRequest();
                }

                var materialToDB = this.mapper.Map<Material>(materialVM);
                this.operationResult = await this.materialService.CreateMaterial(materialToDB);

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

        [HttpPost]
        [Route("CreateBook")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> Create([FromBody] BookViewModel materialVM)
        {
            try
            {
                var material = await this.materialService.GetMaterial(materialVM.Id);

                if (material != null)
                {
                    return BadRequest();
                }

                var materialToDB = this.mapper.Map<Material>(materialVM);
                this.operationResult = await this.materialService.CreateMaterial(materialToDB);

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

        [HttpPost]
        [Route("CreateArticle")]
        [SwaggerResponse(200)]
        [SwaggerResponse(500)]
        public async Task<ActionResult> Create([FromBody] ArticleViewModel materialVM)
        {
            try
            {
                var material = await this.materialService.GetMaterial(materialVM.Id);

                if (material != null)
                {
                    return BadRequest();
                }

                var materialToDB = this.mapper.Map<Material>(materialVM);
                this.operationResult = await this.materialService.CreateMaterial(materialToDB);

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

        [HttpPut("UpdateVideo/{id:int}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> UpdateVideo([FromRoute][Range(1, int.MaxValue)][Required] int id, [FromBody] VideoViewModel videoVM)
        {
            try
            {
                bool materialExist = await this.materialService.ExistMaterial(id);

                if (!materialExist)
                {
                    return NotFound();
                }

                Video video = new Video()
                {
                    Id = id,
                    Duration = videoVM.Duration,
                    Link = videoVM.Link,
                    Name = videoVM.Name,
                    Quality = (Domain.Enums.VideoQuality)videoVM.Quality
                };

                await this.materialService.UpdateMaterial(video);
                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("UpdateBook/{id:int}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> UpdateBook([FromRoute][Range(1, int.MaxValue)][Required] int id, [FromBody] BookViewModel bookVM)
        {
            try
            {
                bool materialExist = await this.materialService.ExistMaterial(id);

                if (!materialExist)
                {
                    return NotFound();
                }

                Book book = new Book()
                {
                    Id = id,
                    Author = bookVM.Author,
                    CountOfPages = bookVM.CountOfPages,
                    Name = bookVM.Name
                };

                await this.materialService.UpdateMaterial(book);
                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("UpdateArticle/{id:int}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> UpdateArticle([FromRoute][Range(1, int.MaxValue)][Required] int id, [FromBody] ArticleViewModel articleVM)
        {
            try
            {
                bool materialExist = await this.materialService.ExistMaterial(id);

                if (!materialExist)
                {
                    return NotFound();
                }

                Article article = new Article()
                {
                    Id = id,
                    Name = articleVM.Name,
                    PublicationDate = articleVM.PublicationDate,
                    Site = articleVM.Site
                };

                await this.materialService.UpdateMaterial(article);
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
