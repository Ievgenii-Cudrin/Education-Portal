using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using EducationPortal.Domain.Entities;

namespace EducationPortal.BLL.Interfaces
{
    public interface IUserSkillSqlService
    {
        Task AddSkillToUser(int userId, int skillId);

        Task<IEnumerable<Skill>> GetAllSkillInUser(int userId);

        Task<int> GetCountOfUserSkill(int userId, int skillId);

        Task<IEnumerable<UserSkill>> GetAllUSerSkillsWithInclude(int userId);
    }
}
