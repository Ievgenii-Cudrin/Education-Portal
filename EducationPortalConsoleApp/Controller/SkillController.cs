namespace EducationPortalConsoleApp.Controller
{
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using EducationPortal.PL.InstanceCreator;
    using EducationPortal.PL.Interfaces;
    using EducationPortal.PL.Mapping;
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Interfaces;

    public class SkillController : ISkillController
    {
        private ISkillService skillService;
        private IMapperService mapperService;

        public SkillController(ISkillService skillService, IMapperService mapper)
        {
            this.skillService = skillService;
            this.mapperService = mapper;
        }

        public Skill CreateSkill()
        {
            SkillViewModel skill = SkillVMInstanceCreator.CreateSkill();

            // mapping
            var skillMap = this.mapperService.CreateMapFromVMToDomain<SkillViewModel, Skill>(skill);

            // create skill
            this.skillService.CreateSkill(skillMap);
            return skillMap;
        }
    }
}
