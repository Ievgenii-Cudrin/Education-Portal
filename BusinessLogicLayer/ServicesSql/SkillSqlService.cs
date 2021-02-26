namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using DataAccessLayer.Repositories;
    using EducationPortal.DAL.Repositories;
    using Entities;

    public class SkillSqlService : ISkillService
    {
        private readonly IRepository<Skill> skillRepository;

        public SkillSqlService(IRepository<Skill> repository)
        {
            this.skillRepository = repository;
        }

        public Skill CreateSkill(Skill skill)
        {
            if (!this.skillRepository.Exist(x => x.Name == skill.Name))
            {
                this.skillRepository.Add(skill);
                this.skillRepository.Save();
                return skill;
            }

            return this.skillRepository.Get(x => x.Name == skill.Name).FirstOrDefault();
        }

        public void Delete(int id)
        {
            if (skillRepository.Exist(x => x.Id == id))
            {
                this.skillRepository.Delete(id);
                this.skillRepository.Save();
            }
        }

        public Skill GetSkill(int id)
        {
            if (this.skillRepository.Exist(x => x.Id == id))
            {
                return this.skillRepository.Get(id);
            }
            else
            {
                return null;
            }
        }

        public Skill GetSkillByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            else
            {
                return this.skillRepository.Get(x => x.Name == name).FirstOrDefault();
            }
        }

        public void UpdateSkill(Skill skill)
        {
            if (skill != null)
            {
                this.skillRepository.Update(skill);
            }
        }

        public bool ExistSkill(int skillId)
        {
            return this.skillRepository.Exist(x => x.Id == skillId);
        }
    }
}
