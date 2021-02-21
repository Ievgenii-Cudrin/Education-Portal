namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.Domain.Comparers;

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
