namespace EducationPortalConsoleApp.Controller
{
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using EducationPortal.PL.InstanceCreator;
    using EducationPortal.PL.Mapping;
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Interfaces;

    public class SkillController : ISkillController
    {
        private ISkillService skillService;

        public SkillController(ISkillService skillService)
        {
            this.skillService = skillService;
        }

        public void CreateSkill()
        {
            SkillViewModel skill = SkillVMInstanceCreator.CreateSkill();

            // mapping
            var skillMap = Mapping.CreateMapFromVMToDomain<SkillViewModel, Skill>(skill);

            // create skill
            this.skillService.CreateSkill(skillMap);
        }
    }
}
