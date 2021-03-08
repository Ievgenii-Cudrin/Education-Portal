using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NLog;

namespace EducationPortal.BLL.ServicesSql
{
    public class CourseSkillService : ICourseSkillService
    {
        private readonly IRepository<CourseSkill> courseSkillRepository;
        private readonly ILogger<CourseSkillService> logger;

        public CourseSkillService(
            IRepository<CourseSkill> courseSkillRepo,
            ILogger<CourseSkillService> logger)
        {
            this.courseSkillRepository = courseSkillRepo;
            this.logger = logger;
        }

        public async Task<bool> AddSkillToCourse(int courseId, int skillId)
        {
            try
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
                    return true;
                }
                else
                {
                    this.logger.LogInformation($"Skill ({skillId}) not added to course ({courseId}). CourseSkill not exist!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Failed add skill to course - {ex.Message}");
                return false;
            }
        }

        public async Task<IList<Skill>> GetAllSkillsFromCourse(int courseId)
        {
            return await this.courseSkillRepository.Get<Skill>(x => x.Skill, x => x.CourseId == courseId);
        }
    }
}
