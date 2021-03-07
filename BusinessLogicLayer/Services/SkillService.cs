namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Linq;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Logging;

    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> skillRepository;

        public SkillService(IRepository<Skill> repository)
        {
            this.skillRepository = repository;
        }

        public void CreateSkill(Skill skill)
        {
            if (!this.skillRepository.Exist(x => x.Name == skill.Name))
            {
                this.skillRepository.Add(skill);
            }
        }

        public void Delete(int id)
        {
            if (skillRepository.Exist(x => x.Id == id))
            {
                this.skillRepository.Delete(id);
            }
        }

        public Skill GetSkill(int id)
        {
            try
            {
                return this.skillRepository.Get(id);
            }
            catch (SqlException)
            {
                return null;
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
