namespace EducationPortal.PL.InstanceCreator
{
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Helpers;

    public static class CourseVMInstanceCreator
    {
        public static CourseViewModel CreateCourse()
        {
            CourseViewModel user = new CourseViewModel()
            {
                Name = GetDataHelper.GetNameFromUser(),
                Description = GetDataHelper.GetDescriptionForCourseFromUser(),
            };

            return user;
        }
    }
}
