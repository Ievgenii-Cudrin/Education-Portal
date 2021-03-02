using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Comparers;

namespace EducationPortal.BLL.ServicesSql
{
    public class CourseComparerService : ICourseComparerService
    {
        private CourseComparer courseComparer;

        public CourseComparer CourseComparer
        {
            get
            {
                if (courseComparer == null)
                {
                    courseComparer = new CourseComparer();
                    return courseComparer;
                }

                return courseComparer;
            }
        }
    }
}
