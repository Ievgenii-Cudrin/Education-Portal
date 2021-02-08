namespace EducationPortal.Domain.Comparers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataAccessLayer.Entities;

    public class CourseComparer : IEqualityComparer<Course>
    {
        public bool Equals([AllowNull] Course x, [AllowNull] Course y)
        {
            if (x != null && y != null)
            {
                return x.Id == y.Id;
            }

            return false;
        }

        public int GetHashCode([DisallowNull] Course obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
