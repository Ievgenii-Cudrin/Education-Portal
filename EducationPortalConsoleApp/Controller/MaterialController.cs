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
        IProgramBranch programmBranch;

        public MaterialController(IMaterialService materialService, IProgramBranch programmBranch)
        {
            this.materialService = materialService;
            this.programmBranch = programmBranch;
        }

        public void CreateNewMaterial()
        {
            MaterialConsoleMessageHelper.ShowTextForChoiceKindOfMaterial();

            string kindOfMaterial = Console.ReadLine();

            switch (kindOfMaterial)
            {
                case "1":
                    CreateVideo();
                    break;
                case "2":
                    CreateArticle();
                    break;
                case "3":
                    //CreateBook();
                    break;
                case "4":
                    programmBranch.SelectFirstStepForAuthorizedUser();
                    break;
                default:
                    CreateNewMaterial();
                    break;
            }
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
