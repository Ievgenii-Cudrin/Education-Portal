namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using DataAccessLayer.Entities;

    public interface IUserSkillSqlService
    {
        Task AddSkillToUser(int userId, int skillId);

        Task<List<Skill>> GetAllSkillInUser(int userId);

        Task<int> GetCountOfUserSkill(int userId, int skillId);
    }
}
