namespace EducationPortalConsoleApp.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using DataAccessLayer.Entities;

    public interface ISkillController
    {
        Task<Skill> CreateSkill();
    }
}
