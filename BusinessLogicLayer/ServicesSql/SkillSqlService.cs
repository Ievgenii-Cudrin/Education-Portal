namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using Entities;

    public class SkillSqlService : ISkillService
    {
        private readonly IRepository<Skill> skillRepository;
        private static IBLLLogger logger;

        public SkillSqlService(IRepository<Skill> repository, IBLLLogger log)
        {
            this.skillRepository = repository;
            logger = log;
        }

        public Skill CreateSkill(Skill skill)
        {
            if (!this.skillRepository.Exist(x => x.Name == skill.Name))
            {
                this.skillRepository.Add(skill);
                this.skillRepository.Save();
                return skill;
                logger.Logger.Debug("Create new skill - " + DateTime.Now);
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

            logger.Logger.Debug("Skill not deleted - " + DateTime.Now);
        }

        public Skill GetSkill(int id)
        {
            if (this.skillRepository.Exist(x => x.Id == id))
            {
                return this.skillRepository.Get(id);
            }
            else
            {
                logger.Logger.Debug("Skill not exist - " + DateTime.Now);
                return null;
            }
        }

        public Skill GetSkillByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                logger.Logger.Debug("method parametr is null or empry - " + DateTime.Now);
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

            logger.Logger.Debug("Skill is null - " + DateTime.Now);
        }

        public bool ExistSkill(int skillId)
        {
            return this.skillRepository.Exist(x => x.Id == skillId);
        }
    }
}
