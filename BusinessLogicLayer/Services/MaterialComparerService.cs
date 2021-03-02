using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Comparers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.BLL.ServicesSql
{
    public class MaterialComparerService : IMaterialComparerService
    {
        MaterialComparer materialComparer;

        public MaterialComparer MaterialComparer
        {
            get
            {
                if (materialComparer == null)
                {
                    materialComparer = new MaterialComparer();
                    return materialComparer;
                }

                return materialComparer;
            }
        }
    }
}
