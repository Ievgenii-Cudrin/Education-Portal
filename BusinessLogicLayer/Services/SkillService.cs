﻿using BusinessLogicLayer.InstanceCreator;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class SkillService : ISkillService
    {
        IRepository<Skill> repository = ProviderServiceBLL.Provider.GetRequiredService<IRepository<Skill>>();
        User authorizedUser;

        public bool CreateSkill(string name)
        {
            //check name, may be we have this skill
            bool uniqueEmail = !repository.GetAll().Any(x => x.Name.ToLower().Equals(name.ToLower()));
            //if name is unique => create new skill, otherwise skill == null
            Skill skill = uniqueEmail ? SkillInstanceCreator.CreateSkill(name) : null;

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

        public bool UpdateSkill(int id, string name)
        {
            Skill skill = repository.Get(id);

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

        public IEnumerable<string> GetAllSkills()
        {
            return repository.GetAll().Select(n => n.Name);
        }

        public bool Delete(int id)
        {
            Skill user = repository.Get(id);

            if (user == null)
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
