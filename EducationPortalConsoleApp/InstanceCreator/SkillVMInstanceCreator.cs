namespace EducationPortal.PL.InstanceCreator
{
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Helpers;

    public static class SkillVMInstanceCreator
    {
        public static SkillViewModel CreateSkill()
        {
            SkillViewModel skill = new SkillViewModel()
            {
                Name = GetDataHelper.GetNameFromUser(),
            };

            return skill;
        }
    }
}
