using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.Models
{
    public class CourseViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<SkillViewModel> Skills = new List<SkillViewModel>();

        public List<MaterialViewModel> Materials = new List<MaterialViewModel>();

    }
}
