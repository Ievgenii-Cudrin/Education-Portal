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
            bool success = skillService.CreateSkill(name);

            if (success)
            {
                Console.WriteLine("Skill successfully created");
                //Add method to continue
            }
            else
            {
                Console.WriteLine("Somthing wrong");
            }
        }

        public void UpdateSkill(int id)
        {
            string name = GetDataHelper.GetNameFromUser();
            bool success = skillService.UpdateSkill(id, name);

            if (success)
            {
                Console.WriteLine("Skill successfully updated");
                //Add method to continue
            }
            else
            {
                Console.WriteLine("Somthing wrong");
            }
        }

        public void Delete(int id)
        {
            bool success = skillService.Delete(id);

            if (success)
            {
                Console.WriteLine("Skill successfully deleted");
                //Add method to continue
            }
            else
            {
                Console.WriteLine("Somthing wrong");
            }
        }
    }
}
