namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using DataAccessLayer.Entities;

    public interface ISkillService
    {
        bool CreateSkill(Skill skill);

        Skill GetSkill(int id);

        bool UpdateSkill(Skill skill);

        IEnumerable<Skill> GetAllSkills();

        Skill GetSkillByName(string name);

        bool Delete(int id);
    }
}
