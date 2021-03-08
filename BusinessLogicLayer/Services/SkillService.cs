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
        private ILogger<SkillService> logger;

        public SkillService(
            IRepository<Skill> repository,
            ILogger<SkillService> logger)
        {
            this.skillRepository = repository;
            this.logger = logger;
        }

        public async Task CreateSkill(Skill skill)
        {
            try
            {
                bool skillExist = await this.skillRepository.Exist(x => x.Name == skill.Name);

                if (!skillExist)
                {
                    await this.skillRepository.Add(skill);
                }
                else
                {
                    this.logger.LogDebug($"Skill ({skill.Name}) not created. Skill exist");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Failed create skill - {ex.Message}");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await this.skillRepository.Delete(id);
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"Skill {id} not deleted - {ex.Message}");
            }
        }

        public async Task<Skill> GetSkill(int id)
        {
            try
            {
                return await this.skillRepository.Get(id);
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"Skill {id} not foound - {ex.Message}");
                return null;
            }
        }

        public async Task<Skill> GetSkillsByPredicate(Expression<Func<Skill, bool>> predicat)
        {
            try
            {
                return await this.skillRepository.GetOne(predicat);
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"Skill by predicat - {predicat.Body} not foound - {ex.Message}");
                return null;
            }
        }

        public async Task UpdateSkill(Skill skill)
        {
            try
            {
                if (skill != null)
                {
                    await this.skillRepository.Update(skill);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"Skill by predicat - {skill.Name} not updated - {ex.Message}");
            }
        }

        public async Task<bool> ExistSkill(int skillId)
        {
            return await this.skillRepository.Exist(x => x.Id == skillId);
        }
    }
}
