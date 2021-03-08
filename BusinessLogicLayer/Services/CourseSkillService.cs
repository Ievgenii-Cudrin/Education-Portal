using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;
using Microsoft.Data.SqlClient;
using NLog;

namespace EducationPortal.BLL.ServicesSql
{
    public class CourseSkillService : ICourseSkillService
    {
        private readonly IRepository<CourseSkill> courseSkillRepository;
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<Skill> skillRepository;

        public CourseSkillService(
            IRepository<CourseSkill> courseSkillRepo,
            IRepository<Skill> skillRepo,
            IRepository<Course> courseRepo)
        {
            this.courseSkillRepository = courseSkillRepo;
            this.courseRepository = courseRepo;
            this.skillRepository = skillRepo;
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
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<IList<Skill>> GetAllSkillsFromCourse(int courseId)
        {
            return await this.courseSkillRepository.Get<Skill>(x => x.Skill, x => x.CourseId == courseId);
        }
    }
}
