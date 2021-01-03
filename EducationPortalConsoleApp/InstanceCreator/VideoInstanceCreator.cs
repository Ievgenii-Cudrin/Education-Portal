using DataAccessLayer.Entities;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.InstanceCreator
{
    public static class VideoInstanceCreator
    {
        public static Video VideoCreator() => new Video()
        {
            Name = GetDataHelper.GetNameFromUser(),
            Duration = GetDataHelper.GetDurationOfVideo(),
            Link = GetDataHelper.GetSiteAddressFromUser(),
            Quality = ""
        };
    }
}
