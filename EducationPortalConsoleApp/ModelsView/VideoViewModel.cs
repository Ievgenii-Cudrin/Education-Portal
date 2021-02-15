namespace EducationPortal.PL.Models
{
    using EducationPortal.PL.EnumViewModel;

    public class VideoViewModel : MaterialViewModel
    {
        public string Link { get; set; }

        public VideoQualityViewModel Quality { get; set; }

        public int Duration { get; set; }

        public override string ToString()
        {
            return $"Type: Video" +
                $"\nName: {this.Name}" +
                $"\nLink: {this.Link}" +
                $"\nVideo quality: {this.Quality}" +
                $"\nVideo duration: {this.Duration}";
        }
    }
}
