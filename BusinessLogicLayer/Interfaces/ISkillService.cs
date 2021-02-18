namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using DataAccessLayer.Entities;

    public interface ISkillService
    {
        void CreateSkill(Skill skill);

        Skill GetSkill(int id);

        void UpdateSkill(Skill skill);

        Skill GetSkillByName(string name);

        void Delete(int id);
    }
}
