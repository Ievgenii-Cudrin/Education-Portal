using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.InstanceCreator
{
    public static class SkillVMInstanceCreator
    {
        public static SkillViewModel CreateSkill()
        {
            SkillViewModel skill = new SkillViewModel()
            {
                Name = GetDataHelper.GetNameFromUser()
            };

            return skill;
        }
    }
}
