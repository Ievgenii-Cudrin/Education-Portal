namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using DataAccessLayer.Entities;

    public interface ISkillService
    {
        void CreateSkill(Skill skill);

        Skill GetSkill(int id);

        void UpdateSkill(Skill skill);

        void Delete(int id);

        bool ExistSkill(int skillId);
    }
}
