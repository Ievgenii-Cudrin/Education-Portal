namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Linq;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;

    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> skillRepository;
        private static IBLLLogger logger;

        public SkillService(IRepository<Skill> repository, IBLLLogger log)
        {
            this.skillRepository = repository;
            logger = log;
        }

        public void CreateSkill(Skill skill)
        {
            if (!this.skillRepository.Exist(x => x.Name == skill.Name))
            {
                this.skillRepository.Add(skill);
                this.skillRepository.Save();
                logger.Logger.Debug("Create new skill - " + DateTime.Now);
            }
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
