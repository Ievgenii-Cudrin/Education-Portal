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

        public bool CreateSkill(Skill skillToCreate)
        {
            //check name, may be we have this skill
            bool uniqueEmail = skillToCreate != null ? !repository.GetAll().Any(x => x.Name.ToLower().Equals(skillToCreate.Name.ToLower())) : false;
            
            //if name is unique => create new skill, otherwise skill == null
            if (uniqueEmail)
            {
                repository.Create(skillToCreate);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool UpdateSkill(Skill skillToUpdate)
        {
            Skill skill = repository.Get(skillToUpdate.Id);

            if (skill == null)
            {
                return false;
            }
            else
            {
                skill.Name = skillToUpdate.Name;
                repository.Update(skill);
            }

            return true;
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            return repository.GetAll();
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
                repository.Delete(id);
            }

            return true;
        }

        public Skill GetSkill(int id)
        {
            return repository.Get(id);
        }

        public Skill GetSkillByName(string name)
        {
            return repository.GetAll().Where(x => x.Name == name).FirstOrDefault();
        }
    }
}
