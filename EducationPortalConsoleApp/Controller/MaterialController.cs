using BusinessLogicLayer.Interfaces;
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

        
        private void CreateVideo()
        {
            string name = GetDataHelper.GetNameFromUser();
            int quality = GetDataHelper.GetVideoQuality();
            int duration = GetDataHelper.GetVideoQuality();
            string link = GetDataHelper.GetSiteAddressFromUser();
            //Create video
            bool success = materialService.CreateVideo(name, link, quality, duration);
            //Show result
            //ProgramConsoleMessageHelper.ShowFunctionResult(success, "Video successfully created");
        }

        private void CreateArticle()
        {
            string name = GetDataHelper.GetNameFromUser();
            DateTime publicationDate = GetDataHelper.GetDateTimeFromUser();
            string site = GetDataHelper.GetSiteAddressFromUser();
            //Create article
            bool success = materialService.CreateArticle(name, site, publicationDate);
            //Show result
            //ProgramConsoleMessageHelper.ShowFunctionResult(success, "Article successfully created");
        }
    }
}
