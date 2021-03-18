using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace EducationPortal.BLL.ServicesSql
{
    public class CourseSkillService : ICourseSkillService
    {
        private readonly IRepository<CourseSkill> courseSkillRepository;
        private readonly ILogger<CourseSkillService> logger;
        private readonly IAuthorizedUser user;
        private IOperationResult operationResult;

        private const string success = "Success";
        private const string skillNotAdded = "Skill not add to course. Skill exist in course";
        private const string skillNotDeletedFromCourse = "Skill not deleted from course";

        public CourseSkillService(
            IRepository<CourseSkill> courseSkillRepo,
            ILogger<CourseSkillService> logger,
            IOperationResult operationResult,
            IAuthorizedUser user)
        {
            this.courseSkillRepository = courseSkillRepo;
            this.logger = logger;
            this.operationResult = operationResult;
            this.user = user;
        }

        public async Task<IOperationResult> AddSkillToCourse(int courseId, int skillId)
        {
            bool userSkillExist = await this.courseSkillRepository.Exist(x => x.CourseId == courseId && x.SkillId == skillId);

            if (!userSkillExist)
            {
                var courseSkill = new CourseSkill()
                {
                    CourseId = courseId,
                    SkillId = skillId,
                };

                await this.courseSkillRepository.Add(courseSkill);
                await this.courseSkillRepository.Save();
                this.logger.LogDebug($"Add skill {skillId} to course ({courseId}) by user ({this.user.User.Id})");
                this.operationResult.IsSucceed = true;
                this.operationResult.Message = success;
            }
            else
            {
                this.logger.LogInformation($"Skill ({skillId}) not added to course ({courseId}). CourseSkill not exist!");
                this.operationResult.IsSucceed = false;
                this.operationResult.Message = skillNotAdded;
            }

            return this.operationResult;
        }

        public async Task<IEnumerable<Skill>> GetAllSkillsFromCourse(int courseId)
        {
            return await this.courseSkillRepository.Get<Skill>(x => x.Skill, x => x.CourseId == courseId);
        }

        public async Task<IOperationResult> DeleteSkillFromCourse(int courseId, int skillId)
        {
            var courseSkill = await this.courseSkillRepository.GetOne(x => x.CourseId == courseId && x.SkillId == skillId);

            if (courseSkill != null)
            {
                await this.courseSkillRepository.Delete(courseSkill);
                await this.courseSkillRepository.Save();
                this.logger.LogInformation($"Skill ({skillId}) deleted from course ({courseId}) by user ({this.user.User.Id})");
                this.operationResult.IsSucceed = true;
                this.operationResult.Message = success;
            }
            else
            {
                this.logger.LogWarning($"Skill ({skillId}) not deleted from course ({courseId}) by user ({this.user.User.Id})");
                this.operationResult.IsSucceed = false;
                this.operationResult.Message = skillNotDeletedFromCourse;
            }

            return this.operationResult;
        }
    }
}
