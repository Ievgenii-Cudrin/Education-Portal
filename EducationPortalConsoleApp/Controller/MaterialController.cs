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

        
        public VideoViewModel CreateVideo()
        {
            VideoViewModel videoVM = VideoVMInstanceCreator.CreateVideo();
            var videoMap = Mapping.CreateMapFromVMToDomain<VideoViewModel, Video>(videoVM);

            bool success = materialService.CreateVideo(videoMap);

            if (success)
            {
                return videoVM;
            }
            else
            {
                return null;
            }
        }

        public ArticleViewModel CreateArticle()
        {
            ArticleViewModel articleVM = ArticleVMInstanceCreator.CreateArticle();
            var articleMap = Mapping.CreateMapFromVMToDomain<ArticleViewModel, Article>(articleVM);

            bool success = materialService.CreateArticle(articleMap);

            if (success)
            {
                return articleVM;
            }
            else
            {
                return null;
            }
        }

        public BookViewModel CreateBook()
        {
            BookViewModel bookVM = BookVMInstanceCreator.CreateBook();
            var bookMap = Mapping.CreateMapFromVMToDomain<BookViewModel, Book>(bookVM);

            bool success = materialService.CreateBook(bookMap);

            if (success)
            {
                return bookVM;
            }
            else
            {
                return null;
            }
        }

        public void UpdateMaterial(int id)
        {
            //ProgramConsoleMessageHelper.ShowFunctionResult(
            //    success,
            //    "Book successfully updated",
            //    "Somthing wrong",
            //    Action,
            //    Action
            //    );
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
