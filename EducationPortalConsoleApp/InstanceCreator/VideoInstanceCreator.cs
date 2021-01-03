using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.InstanceCreator
{
    public static class VideoInstanceCreator
    {
        public static Video VideoCreator() => new Video()
        {
            Name = "",
            Duration = "",
            Link = "",
            Quality = ""
        };
    }
}
