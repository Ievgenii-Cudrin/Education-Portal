using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace EducationPortal.BLL.Interfaces
{
    public interface ICourseSkillService
    {
        Task<IOperationResult> AddSkillToCourse(int courseId, int skillId);

        Task<IEnumerable<Skill>> GetAllSkillsFromCourse(int courseId);

        Task<IOperationResult> DeleteSkillFromCourse(int courseId, int skillId);
    }
}
