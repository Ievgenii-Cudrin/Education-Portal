using EducationPortal.PL.EnumViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.Models
{
    public class VideoViewModel : MaterialViewModel
    {
        public string Link { get; set; }

        public VideoQualityViewModel Quality { get; set; }

        public int Duration { get; set; }

        public override string ToString()
        {
            return $"Type: Video" +
                $"\nName: {Name}" +
                $"\nLink: {Link}" +
                $"\nVideo quality: {Quality}" +
                $"\nVideo duration: {Duration}";
        }
    }
}
