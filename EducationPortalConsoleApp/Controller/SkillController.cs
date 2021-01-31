using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.InstanceCreator;
using EducationPortal.PL.Mapping;
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
            // mapping
            var skillMap = Mapping.CreateMapFromVMToDomain<SkillViewModel, Skill>(skill);
            //create skill
            skillService.CreateSkill(skillMap);
        }
    }
}
