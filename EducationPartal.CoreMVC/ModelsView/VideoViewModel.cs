namespace EducationPartal.CoreMVC.ModelsView
{
    using EducationPartal.CoreMVC.EnumViewModel;
    using EducationPartal.CoreMVC.ModelsView;

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
