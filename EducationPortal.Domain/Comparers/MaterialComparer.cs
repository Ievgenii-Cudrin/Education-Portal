namespace EducationPortal.Domain.Comparers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using global::Entities;

    public class MaterialComparer : IEqualityComparer<Material>
    {
        public bool Equals([AllowNull] Material x, [AllowNull] Material y)
        {
            if (x != null && y != null)
            {
                return x.Id == y.Id && x.Name.ToLower() == y.Name.ToLower();
            }

            return false;
        }

        public int GetHashCode([DisallowNull] Material obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
