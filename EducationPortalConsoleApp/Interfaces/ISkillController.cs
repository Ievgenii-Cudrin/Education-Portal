using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Interfaces
{
    public interface ISkillController
    {
        public void CreateSkill();

        public void UpdateSkill(int id);

        public void Delete(int id);
    }
}
