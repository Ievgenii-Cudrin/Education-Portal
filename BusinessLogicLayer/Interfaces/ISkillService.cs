namespace BusinessLogicLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DataAccessLayer.Entities;

    public interface ISkillService
    {
        Task CreateSkill(Skill skill);

        Task<Skill> GetSkill(int id);

        Task UpdateSkill(Skill skill);

        Task Delete(int id);

        Task<bool> ExistSkill(int skillId);

        Task<Skill> GetSkillsByPredicate(Expression<Func<Skill, bool>> predicat);
    }
}
