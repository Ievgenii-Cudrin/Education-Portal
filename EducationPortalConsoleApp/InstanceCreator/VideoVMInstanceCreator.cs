namespace EducationPortal.PL.InstanceCreator
{
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Helpers;

    public static class VideoVMInstanceCreator
    {
        public static VideoViewModel CreateVideo()
        {
            VideoViewModel video = new VideoViewModel()
            {
                Name = GetDataHelper.GetNameFromUser(),
                Link = GetDataHelper.GetSiteAddressFromUser(),
                Duration = GetDataHelper.GetVideoDuration(),
                Quality = GetDataHelper.GetVideoQuality(),
            };

            return video;
        }
    }
}
