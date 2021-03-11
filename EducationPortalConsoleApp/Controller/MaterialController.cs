using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using EducationPortal.PL.InstanceCreator;
using EducationPortal.PL.Interfaces;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
using Entities;

namespace EducationPortalConsoleApp.Controller
{
    public class MaterialController : IMaterialController
    {
        private readonly IMaterialService materialService;
        private readonly IMapperService mapperService;
        private IOperationResult operationResult;

        public MaterialController(
            IMaterialService materialService,
            IMapperService mapper,
            IOperationResult operationResult)
        {
            this.materialService = materialService;
            this.mapperService = mapper;
            this.operationResult = operationResult;
        }

        public async Task<Material> CreateVideo()
        {
            // create video
            VideoViewModel materialVM = VideoVMInstanceCreator.CreateVideo();

            // mapping
            var videoAfterMap = this.mapperService.CreateMapFromVMToDomain<VideoViewModel, Video>(materialVM);

            // add video to db
            this.operationResult = await this.materialService.CreateMaterial(videoAfterMap);

            if (videoAfterMap != null && this.operationResult.IsSucceed)
            {
                return videoAfterMap;
            }
            else
            {
                return null;
            }
        }

        public async Task<Material> CreateArticle()
        {
            // create article
            ArticleViewModel articleVM = ArticleVMInstanceCreator.CreateArticle();

            // mapping
            var articleAfterMap = this.mapperService.CreateMapFromVMToDomain<ArticleViewModel, Article>(articleVM);

            // add article to db
            this.operationResult = await this.materialService.CreateMaterial(articleAfterMap);

            if (articleAfterMap != null && this.operationResult.IsSucceed)
            {
                return articleAfterMap;
            }
            else
            {
                return null;
            }
        }

        public async Task<Material> CreateBook()
        {
            // create book
            BookViewModel bookVM = BookVMInstanceCreator.CreateBook();

            // mapping
            var bookAfterMap = this.mapperService.CreateMapFromVMToDomain<BookViewModel, Book>(bookVM);

            // add book to db
            this.operationResult = await this.materialService.CreateMaterial(bookAfterMap);

            if (bookAfterMap != null && this.operationResult.IsSucceed)
            {
                return bookAfterMap;
            }
            else
            {
                return null;
            }
        }

        public async Task<Material> GetMaterialFromAllMaterials(int courseId)
        {
            int numberOfPage = 1;
            bool selectedPage = false;

            do
            {
                Console.Clear();
                const int pageSize = 3;
                int recordsCount = await this.materialService.GetCount();
                var pager = new PageInfo(recordsCount, numberOfPage, pageSize);
                int recordsSkip = (numberOfPage - 1) * pageSize;
                var materialsForOnePage = await this.materialService.GetAllMaterialsForOnePage(recordsSkip, pager.PageSize);
                List<MaterialViewModel> materialsVM1 = this.GetAllMaterialVMAfterMappingFromMaterialDomain(materialsForOnePage.ToList());

                // ShowMaterials
                MaterialConsoleMessageHelper.ShowMaterial(materialsVM1);

                Console.WriteLine($"Count of pages - {pager.TotalPages}");
                Console.WriteLine($"Current page - {numberOfPage}");
                Console.WriteLine($"Do you want select another PAGE (enter page) or add MATERIAL (enter material) from this page?");
                string userChoice = Console.ReadLine();

                switch (userChoice.ToLower())
                {
                    case "page":
                        selectedPage = true;
                        Console.WriteLine($"Enter page number: ");
                        numberOfPage = int.Parse(Console.ReadLine());
                        break;
                    case "material":
                        selectedPage = false;
                        break;
                    default:
                        numberOfPage = 1;
                        selectedPage = true;
                        break;
                }
            }
            while (selectedPage);

            Console.Write("\nEnter material id: ");
            int id;

            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                id = 0;
                Console.WriteLine($"Invalid value");
                await this.GetMaterialFromAllMaterials(courseId);
            }

            return await this.materialService.GetMaterial(id);
        }

        public List<MaterialViewModel> GetAllMaterialVMAfterMappingFromMaterialDomain(List<Material> materialsListDomain)
        {
            return this.mapperService.CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(materialsListDomain);
        }

        public void DeleteMaterial(int id)
        {
            throw new NotImplementedException();
        }
    }
}
