namespace BusinessLogicLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.DAL.XML.Repositories;

    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> repository;

        public SkillService(IRepository<Skill> repository)
        {
            this.repository = repository;
        }

        public Skill CreateSkill(Skill skillToCreate)
        {
            // check name, may be we have this skill
            Skill uniqueSkill = this.repository.GetAll().Where(x => x.Name.ToLower().Equals(skillToCreate.Name.ToLower())).FirstOrDefault();

            // if name is unique => create new skill, otherwise skill == null
            if (uniqueSkill == null)
            {
                this.repository.Add(skillToCreate);
                return skillToCreate;
            }

            return uniqueSkill;
        }

        public void UpdateSkill(Skill skillToUpdate)
        {
            Skill skill = this.repository.Get(skillToUpdate.Id);

            if (skill != null)
            {
                skill.Name = skillToUpdate.Name;
                this.repository.Update(skill);
            }
        }

        public void Delete(int id)
        {
            Skill skill = this.repository.Get(id);

            if (skill != null)
            {
                this.repository.Delete(id);
            }
        }

        public Skill GetSkill(int id)
        {
            return this.repository.Get(id);
        }

        public Skill GetSkillByName(string name)
        {
            return this.repository.GetAll().Where(x => x.Name == name).FirstOrDefault();
        }

        public bool ExistSkill(int skillId)
        {
            throw new System.NotImplementedException();
        }
    }
}
