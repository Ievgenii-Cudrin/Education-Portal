using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISkillService : IDeleteEntity
    {
        public bool CreateSkill(Skill skill);

        public bool UpdateSkill(Skill skill);

        public IEnumerable<Skill> GetAllSkills();
    }
}
