using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.InstanceCreator;
using EducationPortal.PL.Models;
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
            SkillViewModel skill = SkillVMInstanceCreator.CreateSkill();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<SkillViewModel, Skill>());
            var mapper = new Mapper(config);
            // сопоставление
            var skillMap = mapper.Map<SkillViewModel, Skill>(skill);

            bool success = skillService.CreateSkill(skillMap);
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
