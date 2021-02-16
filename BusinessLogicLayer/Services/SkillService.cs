namespace BusinessLogicLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;

    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> repository;

        public SkillService(IRepository<Skill> repository)
        {
            this.repository = repository;
        }

        public bool CreateSkill(Skill skillToCreate)
        {
            // check name, may be we have this skill
            bool uniqueEmail = skillToCreate != null && !this.repository.GetAll().Any(x => x.Name.ToLower().Equals(skillToCreate.Name.ToLower()));

            // if name is unique => create new skill, otherwise skill == null
            if (uniqueEmail)
            {
                this.repository.Add(skillToCreate);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool UpdateSkill(Skill skillToUpdate)
        {
            Skill skill = this.repository.Get(skillToUpdate.Id);

            if (skill == null)
            {
                return false;
            }
            else
            {
                skill.Name = skillToUpdate.Name;
                this.repository.Update(skill);
            }

            return true;
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            return this.repository.GetAll();
        }

        public bool Delete(int id)
        {
            Skill skill = this.repository.Get(id);

            if (skill == null)
            {
                return false;
            }
            else
            {
                this.repository.Delete(id);
            }

            return true;
        }

        public Skill GetSkill(int id)
        {
            return this.repository.Get(id);
        }

        public Skill GetSkillByName(string name)
        {
            return this.repository.GetAll().Where(x => x.Name == name).FirstOrDefault();
        }
    }
}
