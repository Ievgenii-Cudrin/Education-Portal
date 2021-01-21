using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.Models
{
    public class MaterialViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }

        public bool IsPassed { get; set; }
    }
}
