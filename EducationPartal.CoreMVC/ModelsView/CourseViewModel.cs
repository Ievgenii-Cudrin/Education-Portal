using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPartal.CoreMVC.ModelsView
{
    public class CourseViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<SkillViewModel> Skills { get; set; }

        public List<MaterialViewModel> Materials { get; set; }

        public string Completed { get; set; }
    }
}
