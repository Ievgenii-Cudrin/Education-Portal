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

            //bool createVideo = materialService.CreateVideo(name, quality, duration, link);
        }

        private void CreateArticle()
        {

        }
    }
}
