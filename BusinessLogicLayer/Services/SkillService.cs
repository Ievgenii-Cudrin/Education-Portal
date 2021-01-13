using BusinessLogicLayer.InstanceCreator;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class SkillService
    {
        IUnitOfWork uow;

        public SkillService()
        {
            this.uow = new EFUnitOfWork();
        }

        public bool CreateSkill(string name, string password, string email, string phoneNumber)
        {
            bool uniqueSkill = uow.Skills.GetAll().Any(x => x.Name.ToLower().Equals(name.ToLower()));
            Skill skill = uniqueSkill ? SkillInstanceCreator.CreateSkill(name) : null;

            if (skill != null)
                uow.Skills.Create(skill);
            else
                return false;

            return true;
        }

        public bool UpdateSkill(int id, string name)
        {
            Skill skill = uow.Skills.Get(id);
            if (skill == null)
            {
                return false;
            }
            else
            {
                skill.Name = name;
                uow.Skills.Update(skill);
                return true;
            }
        }

        public IEnumerable<string> GetAllUsers()
        {
            return uow.Skills.GetAll().Select(n => n.Name);
        }

        public bool DeleteUser(int id)
        {
            Skill skill = uow.Skills.Get(id);
            if (skill == null)
            {
                return false;
            }
            else
            {
                uow.Skills.Delete(Convert.ToInt32(id));
                return true;
            }
        }
    }
}
