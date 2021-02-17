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
        private readonly IRepository<Skill> repository;

        public SkillSqlService(IEnumerable<IRepository<Skill>> repository)
        {
            this.repository = repository.FirstOrDefault(t => t.GetType() == typeof(RepositoryXml<Skill>));
        }

        public bool CreateSkill(Skill skill)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            throw new NotImplementedException();
        }

        public Skill GetSkill(int id)
        {
            throw new NotImplementedException();
        }

        public Skill GetSkillByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSkill(Skill skill)
        {
            throw new NotImplementedException();
        }
    }
}
