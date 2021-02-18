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
    using Entities;

    public class SkillSqlService : ISkillService
    {
        private readonly IRepository<Skill> skillRepository;

        public SkillSqlService(IEnumerable<IRepository<Skill>> repository)
        {
            this.skillRepository = repository.FirstOrDefault(t => t.GetType() == typeof(RepositoryXml<Skill>));
        }

        public void CreateSkill(Skill skill)
        {
            bool uniqueSkill = skill != null && !this.skillRepository.Exist(x => x.Name.ToLower().Equals(skill.Name.ToLower()));

            if (uniqueSkill)
            {
                this.skillRepository.Add(skill);
                this.skillRepository.Save();
            }
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
    }
}
