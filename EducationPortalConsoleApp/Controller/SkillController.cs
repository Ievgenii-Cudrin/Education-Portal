namespace EducationPortalConsoleApp.Controller
{
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using EducationPortal.PL.InstanceCreator;
    using EducationPortal.PL.Interfaces;
    using EducationPortal.PL.Mapping;
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Interfaces;
    using System.Linq;
    using System.Threading.Tasks;

    public class SkillController : ISkillController
    {
        private ISkillService skillService;
        private IMapperService mapperService;

        public SkillController(ISkillService skillService, IMapperService mapper)
        {
            this.skillService = skillService;
            this.mapperService = mapper;
        }

        public async Task<Skill> CreateSkill()
        {
            SkillViewModel skill = SkillVMInstanceCreator.CreateSkill();
            var existingSkill = await this.skillService.GetSkillsByPredicate(x => x.Name == skill.Name);

            if (existingSkill != null)
            {
                skill.Id = existingSkill.Id;
            }

            // mapping
            var skillMap = this.mapperService.CreateMapFromVMToDomain<SkillViewModel, Skill>(skill);

            // create skill
            await this.skillService.CreateSkill(skillMap);
            return skillMap;
        }
    }
}
