namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using DataAccessLayer.Entities;

    public interface ICourseSkillService
    {
        Task<bool> AddSkillToCourse(int courseId, int skillId);

        Task<IList<Skill>> GetAllSkillsFromCourse(int courseId);
    }
}
