using BusinessLogicLayer.Interfaces;
using EducationPartal.CoreMVC.Interfaces;
using EducationPartal.CoreMVC.ModelsView;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.CoreMVC.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        private readonly IMaterialService materialService;
        private readonly IAuthorizedUser authorizedUser;
        private readonly IAutoMapperService mapperService;
        private readonly ICourseMaterialService courseMaterialService;
        private readonly ICourseService courseService;
        private IOperationResult operationResult;

        public MaterialController(
            IMaterialService materialService,
            IAuthorizedUser authorizedUser,
            IAutoMapperService mapperService,
            ICourseMaterialService courseMaterialService,
            ICourseService courseService,
            IOperationResult operationResult)
        {
            this.materialService = materialService;
            this.authorizedUser = authorizedUser;
            this.mapperService = mapperService;
            this.courseMaterialService = courseMaterialService;
            this.courseService = courseService;
            this.operationResult = operationResult;
        }
        // GET: MaterialController
        public async Task<ActionResult> Index(int id = 1)
        {
            const int pageSize = 3;

            if (id < 1)
            {
                id = 1;
            }

            int materialsCount = await this.materialService.GetCount();
            var pager = new PageInfo(materialsCount, id, pageSize);
            int materialsSkip = (id - 1) * pageSize;
            var materialsFromDbForOnePage = await this.materialService.GetAllMaterialsForOnePage(materialsSkip, pager.PageSize);
            var materialsViewModel = this.mapperService.
                CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(materialsFromDbForOnePage.ToList());

            this.ViewBag.Pager = pager;

            return View(materialsViewModel);
        }

        public async Task<ActionResult> AddMaterialToCourse(int id)
        {
            int courseId = await this.courseService.GetLastId();
            this.operationResult = await this.courseMaterialService.AddMaterialToCourse(courseId, id);

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

        public async Task<ActionResult> DeleteMaterialFromCourse(int id)
        {
            int courseId = await this.courseService.GetLastId();
            this.operationResult = await this.courseMaterialService.DeleteMaterialFromCourse(courseId, id);

            if (operationResult.IsSucceed)
            {
                return RedirectToAction("ContinueCourseCreating", "Course");
            }
            else
            {
                ViewData["Message"] = this.operationResult.Message;
                return View();
            }
        }


        // GET: MaterialController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MaterialController/Create
        public async Task<ActionResult> CreateVideo()
        {
            return View();
        }

        // POST: MaterialController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateVideo(VideoViewModel videoVM)
        {
            if (ModelState.IsValid)
            {
                var materialDomain = this.mapperService
                    .CreateOneMapFromVMToDomainWithIncludeMaterialType<MaterialViewModel, Material, VideoViewModel, Video, ArticleViewModel, Article, BookViewModel, Book>(videoVM);

                //Operation result
                this.operationResult = await this.materialService.CreateMaterial(materialDomain);

                if (operationResult.IsSucceed)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Error");
                }
            }

            return View();
        }

        public async Task<ActionResult> CreateBook()
        {
            return View();
        }

        // POST: MaterialController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBook(BookViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                var bookDomain = this.mapperService
                    .CreateOneMapFromVMToDomainWithIncludeMaterialType<MaterialViewModel, Material, VideoViewModel, Video, ArticleViewModel, Article, BookViewModel, Book>(bookVM);

                //Operation result
                this.operationResult = await this.materialService.CreateMaterial(bookDomain);

                if (this.operationResult.IsSucceed)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Error");
                }
            }

            return View();
        }

        public async Task<ActionResult> CreateArticle()
        {
            return View();
        }

        // POST: MaterialController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateArticle(ArticleViewModel articleVM)
        {
            if (ModelState.IsValid)
            {
                var articleDomain = this.mapperService
                    .CreateOneMapFromVMToDomainWithIncludeMaterialType<MaterialViewModel, Material, VideoViewModel, Video, ArticleViewModel, Article, BookViewModel, Book>(articleVM);

                //Operation result
                this.operationResult = await this.materialService.CreateMaterial(articleDomain);

                if (this.operationResult.IsSucceed)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Error");
                }
            }
            return View();
        }

        // GET: MaterialController/Edit/5
        public async Task<ActionResult> EditArticle(int id)
        {
            var article = await this.materialService.GetMaterial(id);
            var articleVM = this.mapperService
                        .CreateOneMapFromVMToDomainWithIncludeMaterialType< Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book , BookViewModel>(article);

            return View(articleVM);
        }

        // POST: MaterialController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditArticle(int id, ArticleViewModel articleVM)
        {
            try
            {
                var articleDomain = this.mapperService
                        .CreateOneMapFromVMToDomainWithIncludeMaterialType<MaterialViewModel, Material, VideoViewModel, Video, ArticleViewModel, Article, BookViewModel, Book>(articleVM);
                
                await this.materialService.UpdateMaterial(articleDomain);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> EditBook(int id)
        {
            var bookDomain = await this.materialService.GetMaterial(id);
            var bookVM = this.mapperService
                        .CreateOneMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(bookDomain);

            return View(bookVM);
        }

        // POST: MaterialController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBook(int id, BookViewModel bookVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bookDomain = this.mapperService
                        .CreateOneMapFromVMToDomainWithIncludeMaterialType<MaterialViewModel, Material, VideoViewModel, Video, ArticleViewModel, Article, BookViewModel, Book>(bookVM);
                    
                    await this.materialService.UpdateMaterial(bookDomain);
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> EditVideo(int id)
        {
            var videoDomain = await this.materialService.GetMaterial(id);
            var videoVM = this.mapperService.CreateOneMapFromVMToDomainWithIncludeMaterialType
                <Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(videoDomain);

            return View(videoVM);
        }

        // POST: MaterialController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditVideo(int id, VideoViewModel videoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var videoDomain = this.mapperService.CreateOneMapFromVMToDomainWithIncludeMaterialType
                        <MaterialViewModel, Material, VideoViewModel, Video, ArticleViewModel, Article, BookViewModel, Book>(videoVM);
                    
                    await this.materialService.UpdateMaterial(videoDomain);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: MaterialController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MaterialController/Delete/5
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
