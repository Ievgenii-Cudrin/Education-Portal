namespace EducationPortalConsoleApp.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogicLayer.Interfaces;
    using EducationPortal.PL.InstanceCreator;
    using EducationPortal.PL.Mapping;
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Helpers;
    using EducationPortalConsoleApp.Interfaces;
    using Entities;

    public class MaterialController : IMaterialController
    {
        private readonly IMaterialService materialService;

        public MaterialController(IMaterialService materialService)
        {
            this.materialService = materialService;
        }

        public Material CreateVideo()
        {
            // create video
            VideoViewModel materialVM = VideoVMInstanceCreator.CreateVideo();

            // mapping
            var videoMap = Mapping.CreateMapFromVMToDomainWithIncludeVideoType<MaterialViewModel, Material, VideoViewModel, Video>(materialVM);

            // add video to db
            bool success = this.materialService.CreateVideo(Mapping.CreateMapFromVMToDomain<VideoViewModel, Video>(materialVM));

            if (success)
            {
                return videoMap;
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
            var articleMap = Mapping.CreateMapFromVMToDomainWithIncludeVideoType<MaterialViewModel, Material, ArticleViewModel, Article>(articleVM);

            // add article to db
            bool success = this.materialService.CreateArticle(Mapping.CreateMapFromVMToDomain<ArticleViewModel, Article>(articleVM));

            if (success)
            {
                return articleMap;
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
            var bookMap = Mapping.CreateMapFromVMToDomainWithIncludeVideoType<MaterialViewModel, Material, BookViewModel, Book>(bookVM);

            // add book to db
            bool success = this.materialService.CreateBook(Mapping.CreateMapFromVMToDomain<BookViewModel, Book>(bookVM));

            if (success)
            {
                return bookMap;
            }
            else
            {
                return null;
            }
        }

        public Material GetMaterialFromAllMaterials()
        {
            // mapping from domain to viewmodel
            List<MaterialViewModel> materialsVM1 = this.GetAllMaterialVMAfterMappingFromMaterialDomain(this.materialService.GetAllMaterials().ToList());

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
                this.GetMaterialFromAllMaterials();
            }

            return this.materialService.GetMaterial(id);
        }

        public List<MaterialViewModel> GetAllMaterialVMAfterMappingFromMaterialDomain(List<Material> materialsListDomain)
        {
            return Mapping.CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(materialsListDomain);
        }

        public void DeleteMaterial(int id)
        {
            throw new NotImplementedException();
        }
    }
}
