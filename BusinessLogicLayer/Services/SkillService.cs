namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
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

        public async Task CreateSkill(Skill skill)
        {
            bool skillExist = await this.skillRepository.Exist(x => x.Name == skill.Name);

            if (!skillExist)
            {
                await this.skillRepository.Add(skill);
            }
        }

        public async Task Delete(int id)
        {
            if (await this.skillRepository.Exist(x => x.Id == id))
            {
                await this.skillRepository.Delete(id);
            }
        }

        public async Task<Skill> GetSkill(int id)
        {
            try
            {
                return await this.skillRepository.Get(id);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task<Skill> GetSkillsByPredicate(Expression<Func<Skill, bool>> predicat)
        {
            try
            {
                return await this.skillRepository.GetOne(predicat);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task UpdateSkill(Skill skill)
        {
            if (skill != null)
            {
                await this.skillRepository.Update(skill);
            }
        }

        public async Task<bool> ExistSkill(int skillId)
        {
            return await this.skillRepository.Exist(x => x.Id == skillId);
        }
    }
}
