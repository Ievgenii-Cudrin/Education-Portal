using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.InstanceCreator
{
    public static class SkillInstanceCreator
    {
        public static Skill CreateSkill(string name)
        {
            Skill skill = null;

            if(name != null && name.Length > 2)
            {
                skill = new Skill()
                {
                    Name = name,
                    CountOfPoint = 0
                };
            }

            return skill;
        }
    }
}
