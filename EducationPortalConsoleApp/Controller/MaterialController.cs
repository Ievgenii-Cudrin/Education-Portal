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
using System.Text;

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

        public void DeleteMaterial(int id)
        {
            materialService.Delete(id);

            //ProgramConsoleMessageHelper.ShowFunctionResult(
            //    success,
            //    "Book successfully deleted",
            //    "Somthing wrong",
            //    Action,
            //    Action
            //    );
        }
    }
}
