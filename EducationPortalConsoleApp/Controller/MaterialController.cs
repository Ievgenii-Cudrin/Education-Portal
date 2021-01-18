﻿using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.InstanceCreator;
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

        
        public void CreateVideo()
        {
            VideoViewModel video = VideoVMInstanceCreator.CreateVideo();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<VideoViewModel, Video>());
            var mapper = new Mapper(config);
            // сопоставление
            var videoMap = mapper.Map<VideoViewModel, Video>(video);

            bool success = materialService.CreateVideo(videoMap);

            //Create video
            //bool success = materialService.CreateVideo(name, link, quality, duration);
            //Show result
            //ProgramConsoleMessageHelper.ShowFunctionResult(success, "Video successfully created");
        }

        public void CreateArticle()
        {
            string name = GetDataHelper.GetNameFromUser();
            DateTime publicationDate = GetDataHelper.GetDateTimeFromUser();
            string site = GetDataHelper.GetSiteAddressFromUser();
            //Create article
            //bool success = materialService.CreateArticle(name, site, publicationDate);

            //Show result
            //ProgramConsoleMessageHelper.ShowFunctionResult(
            //    success, 
            //    "Article successfully created",
            //    "",
            //    Action,
            //    Action
            //    );
        }

        public void CreateBook()
        {
            string name = GetDataHelper.GetNameFromUser();
            int countOfPages = GetDataHelper.GetCountOfBookPages();
            string authorName = GetDataHelper.GetAuthorNameFromUser();
            //bool success = materialService.CreateBook(name, authorName, countOfPages);

            //ProgramConsoleMessageHelper.ShowFunctionResult(
            //    success,
            //    "Book successfully created",
            //    "Somthing wrong",
            //    Action,
            //    Action
            //    );
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
