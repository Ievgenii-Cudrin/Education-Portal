using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISkillService : IDeleteEntity
    {
        public bool CreateSkill(string name);

        public bool UpdateSkill(int id, string name);

        public IEnumerable<string> GetAllSkills();
    }
}
