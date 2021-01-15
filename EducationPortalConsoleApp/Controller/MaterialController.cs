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
            bool success = materialService.CreateVideo(name, link, quality, duration);

            if (success)
            {
                Console.WriteLine("Video successfully created");
            }
            else
            {
                Console.WriteLine("Somthing wrong");
            }
        }

        private void CreateArticle()
        {
            string name = GetDataHelper.GetNameFromUser();
            DateTime publicationDate = GetDataHelper.GetDateTimeFromUser();
            string site = GetDataHelper.GetSiteAddressFromUser();
            bool success = materialService.CreateArticle(name, site, publicationDate);

            if (success)
            {
                Console.WriteLine();
            }

        }
    }
}
