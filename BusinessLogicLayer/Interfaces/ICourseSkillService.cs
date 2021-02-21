namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataAccessLayer.Entities;

    public interface ICourseSkillService
    {
        bool AddSkillToCourse(int courseId, int skillId);

        List<Skill> GetAllSkillsFromCourse(int courseId);
    }
}
