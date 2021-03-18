using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace EducationPortalConsoleApp.Interfaces
{
    public interface ISkillController
    {
        Task<Skill> CreateSkill();
    }
}
