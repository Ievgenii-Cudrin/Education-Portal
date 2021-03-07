namespace EducationPortalConsoleApp.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogicLayer.Interfaces;
    using EducationPortal.PL.InstanceCreator;
    using EducationPortal.PL.Interfaces;
    using EducationPortal.PL.Mapping;
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Helpers;
    using EducationPortalConsoleApp.Interfaces;
    using Entities;

    public class MaterialController : IMaterialController
    {
        private readonly IMaterialService materialService;
        private IMapperService mapperService;

        public MaterialController(IMaterialService materialService, IMapperService mapper)
        {
            this.materialService = materialService;
            this.mapperService = mapper;
        }

        public Material CreateVideo()
        {
            // create video
            VideoViewModel materialVM = VideoVMInstanceCreator.CreateVideo();

            // mapping
            var videoAfterMap = this.mapperService.CreateMapFromVMToDomain<VideoViewModel, Video>(materialVM);

            // add video to db
            bool succsess = this.materialService.CreateMaterial(videoAfterMap);

            if (videoAfterMap != null && succsess)
            {
                return videoAfterMap;
            }
            else
            {
                return null;
            }
        }

        public Material CreateArticle()
        {
            // create article
            ArticleViewModel articleVM = ArticleVMInstanceCreator.CreateArticle();

            // mapping
            var articleAfterMap = this.mapperService.CreateMapFromVMToDomain<ArticleViewModel, Article>(articleVM);

            // add article to db
            bool succsess = this.materialService.CreateMaterial(articleAfterMap);

            if (articleAfterMap != null && succsess)
            {
                return articleAfterMap;
            }
            else
            {
                return null;
            }
        }

        public Material CreateBook()
        {
            // create book
            BookViewModel bookVM = BookVMInstanceCreator.CreateBook();

            // mapping
            var bookAfterMap = this.mapperService.CreateMapFromVMToDomain<BookViewModel, Book>(bookVM);

            // add book to db
            bool success = this.materialService.CreateMaterial(bookAfterMap);

            if (bookAfterMap != null && success)
            {
                return bookAfterMap;
            }
            else
            {
                return null;
            }
        }

        public Material GetMaterialFromAllMaterials(int courseId)
        {
            // mapping from domain to viewmodel
            List<MaterialViewModel> materialsVM1 = this.GetAllMaterialVMAfterMappingFromMaterialDomain(this.materialService.GetAllExceptedMaterials(courseId).ToList());

            // ShowMaterials
            MaterialConsoleMessageHelper.ShowMaterial(materialsVM1);
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
                this.GetMaterialFromAllMaterials(courseId);
            }

            return this.materialService.GetMaterial(id);
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
