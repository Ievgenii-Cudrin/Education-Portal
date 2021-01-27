using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.InstanceCreator;
using EducationPortal.PL.Mapping;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
using EducationPortalConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationPortalConsoleApp.Controller
{
    public class MaterialController : IMaterialController
    {
        IMaterialService materialService;

        public MaterialController(IMaterialService materialService)
        {
            this.materialService = materialService;
        }

        
        public Material CreateVideo()
        {
            VideoViewModel materialVM = VideoVMInstanceCreator.CreateVideo();
            var videoMap = Mapping.CreateMapFromVMToDomainWithIncludeVideoType<MaterialViewModel, Material, VideoViewModel, Video>(materialVM);

            bool success = materialService.CreateVideo(Mapping.CreateMapFromVMToDomain<VideoViewModel, Video>(materialVM));

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
            ArticleViewModel articleVM = ArticleVMInstanceCreator.CreateArticle();
            var articleMap = Mapping.CreateMapFromVMToDomainWithIncludeVideoType<MaterialViewModel, Material, ArticleViewModel, Article>(articleVM);

            bool success = materialService.CreateArticle(Mapping.CreateMapFromVMToDomain<ArticleViewModel, Article>(articleVM));

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
            BookViewModel bookVM = BookVMInstanceCreator.CreateBook();
            var bookMap = Mapping.CreateMapFromVMToDomainWithIncludeVideoType<MaterialViewModel, Material, BookViewModel, Book>(bookVM);

            bool success = materialService.CreateBook(Mapping.CreateMapFromVMToDomain<BookViewModel, Book>(bookVM));

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
            //MD, MV, VD, VV, AD, AV, BD, BV
            List<MaterialViewModel> materialsVM1 = GetAllMaterialVMAfterMappingFromMaterialDomain(materialService.GetAllMaterials().ToList());
            foreach (var materialVM in materialsVM1)
            {
                Console.WriteLine($"Id: {materialVM.Id}, {materialVM.ToString()}\n");
            }
            Console.Write("\nEnter material id: ");
            int id;
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch(Exception ex)
            {
                id = 0;
                Console.WriteLine($"Invalid value - {ex.Message}");
                GetMaterialFromAllMaterials();
            }

            return materialService.GetMaterial(id);
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
