using BusinessLogicLayer.InstanceCreator;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class SkillService : ISkillService
    {
        IRepository<Skill> repository;

        public SkillService(IRepository<Skill> repository)
        {
            this.repository = repository;
        }

        public bool CreateSkill(string name)
        {
            //check for uniqueness
            bool uniqueSkill = repository.GetAll().Any(x => x.Name.ToLower().Equals(name.ToLower()));
            Skill skill = uniqueSkill ? SkillInstanceCreator.CreateSkill(name) : null;

            if (skill != null)
            {
                repository.Create(skill);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool UpdateSkill(string name)
        {
            Skill skill = repository.GetAll().Where(x => x.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();

            if (skill == null)
            {
                return false;
            }
            else
            {
                skill.Name = name;
                repository.Update(skill);
            }

            return true;
        }

        public IEnumerable<string> GetAllUsers()
        {
            return repository.GetAll().Select(n => n.Name);
        }

        public bool Delete(int id)
        {
            Skill skill = repository.Get(id);
            if (skill == null)
            {
                return false;
            }
            else
            {
                repository.Delete(Convert.ToInt32(id));
            }

            return true;
        }
    }
}
