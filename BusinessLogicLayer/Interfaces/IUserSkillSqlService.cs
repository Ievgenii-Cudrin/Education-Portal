namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataAccessLayer.Entities;

    public interface IUserSkillSqlService
    {
        void AddSkillToUser(int userId, int skillId);

        List<Skill> GetAllSkillInUser(int userId);
    }
}
