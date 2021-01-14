using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISkillService : IDeleteEntity
    {
        public bool CreateSkill(string name);

        public bool UpdateSkill(string name);

        public IEnumerable<string> GetAllUsers();
    }
}
