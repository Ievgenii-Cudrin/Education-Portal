using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using EducationPortal.BLL.Interfaces;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISkillService
    {
        Task<IOperationResult> CreateSkill(Skill skill);

        Task<Skill> GetSkill(int id);

        Task UpdateSkill(Skill skill);

        Task Delete(int id);

        Task<bool> ExistSkill(int skillId);

        Task<Skill> GetSkillsByPredicate(Expression<Func<Skill, bool>> predicat);

        Task<IEnumerable<Skill>> GetAllSkillsForOnePage(int take, int skip);

        Task<int> GetCount();
    }
}
