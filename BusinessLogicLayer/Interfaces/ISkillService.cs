namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using DataAccessLayer.Entities;

    public interface ISkillService : IDeleteEntity
    {
        public bool CreateSkill(Skill skill);

        public Skill GetSkill(int id);

        public bool UpdateSkill(Skill skill);

        public IEnumerable<Skill> GetAllSkills();

        public Skill GetSkillByName(string name);
    }
}
