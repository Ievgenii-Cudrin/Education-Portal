using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.InstanceCreator
{
    public static class VideoInstanceCreator
    {
        public static Video CreateVideo(string name, string link, int quality, int duration)
        {
            Video video = null;

            if (name != null && link != null && quality != 0 && duration != 0)
            {
                video = new Video()
                {
                    Name = name,
                    Link = link,
                    Duration = duration,
                    Quality = quality
                    
                };
            }

            return video == null ? null : video;
        }
    }
}
