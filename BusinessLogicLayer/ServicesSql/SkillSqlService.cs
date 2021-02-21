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

        public SkillSqlService(IEnumerable<IRepository<Skill>> repository)
        {
            this.skillRepository = repository.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<Skill>));
        }

        public Skill CreateSkill(Skill skill)
        {
            Skill uniqueSkill = this.skillRepository.Get(x => x.Name.ToLower().Equals(skill.Name.ToLower())).FirstOrDefault();

            if (uniqueSkill == null)
            {
                this.skillRepository.Add(skill);
                this.skillRepository.Save();
                return skill;
            }

            return uniqueSkill;
        }

        public void Delete(int id)
        {
            this.skillRepository.Delete(id);
            this.skillRepository.Save();
        }

        public Skill GetSkill(int id)
        {
            return this.skillRepository.Get(id);
        }

        public Skill GetSkillByName(string name)
        {
            return this.skillRepository.Get(x => x.Name == name).FirstOrDefault();
        }

        public void UpdateSkill(Skill skill)
        {
            this.skillRepository.Update(skill);
        }

        public bool ExistSkill(int skillId)
        {
            return this.skillRepository.Exist(x => x.Id == skillId);
        }
    }
}
