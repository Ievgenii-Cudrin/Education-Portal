using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationPortal.BLL.ServicesSql
{
    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> skillRepository;
        private readonly ILogger<SkillService> logger;
        private readonly IAuthorizedUser authorizedUser;
        private IOperationResult operationResult;

        private const string success = "Success";
        private const string skillExistInDB = "Skill with this name exist";

        public SkillService(
            IRepository<Skill> repository,
            ILogger<SkillService> logger,
            IOperationResult operationResult,
            IAuthorizedUser authorizedUser)
        {
            this.skillRepository = repository;
            this.logger = logger;
            this.operationResult = operationResult;
            this.authorizedUser = authorizedUser;
        }

        public async Task<IOperationResult> CreateSkill(Skill skill)
        {
            bool skillExist = await this.skillRepository.Exist(x => x.Name == skill.Name);

            if (!skillExist)
            {
                await this.skillRepository.Add(skill);
                this.logger.LogDebug($"Skill ({skill.Name}) successfullu created by user {this.authorizedUser.User.Id}");
                this.operationResult.Message = success;
                this.operationResult.IsSucceed = true;
            }
            else
            {
                this.operationResult.Message = skillExistInDB;
                this.operationResult.IsSucceed = false;
                this.logger.LogDebug($"Skill ({skill.Name}) not created. Skill exist");
            }

            await this.skillRepository.Save();

            return this.operationResult;
        }

        public async Task Delete(int id)
        {
            try
            {
                await this.skillRepository.Delete(id);
                await this.skillRepository.Save();
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
                    await this.skillRepository.Save();
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

        public async Task<IEnumerable<Skill>> GetAllSkillsForOnePage(int take, int skip)
        {
            return await this.skillRepository.GetPage(take, skip);
        }

        public async Task<int> GetCount()
        {
            return await this.skillRepository.Count();
        }
    }
}
