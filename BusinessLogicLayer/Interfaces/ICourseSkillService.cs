namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICourseSkillService
    {
        bool AddSkillToCourse(int courseId, int skillId);
    }
}
