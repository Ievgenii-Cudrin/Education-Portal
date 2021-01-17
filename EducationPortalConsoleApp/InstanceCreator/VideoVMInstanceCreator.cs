using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.InstanceCreator
{
    public static class VideoVMInstanceCreator
    {
        public static VideoViewModel CreateBook()
        {
            VideoViewModel video = new VideoViewModel()
            {
                Name = GetDataHelper.GetNameFromUser(),
                Link = GetDataHelper.GetSiteAddressFromUser(),
                Duration = GetDataHelper.GetVideoDuration(),
                Quality = GetDataHelper.GetVideoQuality()
            };

            return video;
        }
    }
}
