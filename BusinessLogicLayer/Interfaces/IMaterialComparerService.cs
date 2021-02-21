namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EducationPortal.Domain.Comparers;

    public interface IMaterialComparerService
    {
        MaterialComparer MaterialComparer { get; }
    }
}
