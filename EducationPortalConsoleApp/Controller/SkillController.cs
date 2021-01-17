using BusinessLogicLayer.Interfaces;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Controller
{
    public class SkillController : ISkillController
    {
        ISkillService skillService;

        public SkillController(ISkillService skillService)
        {
            this.skillService = skillService;
        }

        public void CreateSkill()
        {
            string name = GetDataHelper.GetNameFromUser();
            //bool success = skillService.CreateSkill(name);
            //ProgramConsoleMessageHelper.ShowFunctionResult(success, "Skill successfully created");

            //Add func to continue
        }

        public void UpdateSkill(int id)
        {
            string name = GetDataHelper.GetNameFromUser();
            //bool success = skillService.UpdateSkill(id, name);

            //Add method to do
            //ProgramConsoleMessageHelper.ShowFunctionResult(success, "Skill successfully updated", ()=> { }, () => { });
        }

        public void Delete(int id)
        {
            bool success = skillService.Delete(id);

            //add method to do
            //ProgramConsoleMessageHelper.ShowFunctionResult(success, "Skill successfully deleted");
        }
    }
}
