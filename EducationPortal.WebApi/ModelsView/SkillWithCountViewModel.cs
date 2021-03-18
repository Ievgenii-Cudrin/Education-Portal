using EducationPartal.WebApi.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.ModelsView
{
    public class SkillWithCountViewModel : SkillViewModel
    {
        public int CountOfPoint { get; set; }

        public SkillViewModel Skill { get; set; }
    }
}
