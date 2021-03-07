using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool AddSkillToCourse(int courseId, int skillId)
        {
            try
            {
                if (!this.courseSkillRepository.Exist(x => x.CourseId == courseId && x.SkillId == skillId))
                {
                    var courseSkill = new CourseSkill()
                    {
                        CourseId = courseId,
                        SkillId = skillId,
                    };

                    this.courseSkillRepository.Add(courseSkill);
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

        public List<Skill> GetAllSkillsFromCourse(int courseId)
        {
            return this.courseSkillRepository.Get<Skill>(x => x.Skill, x => x.CourseId == courseId).ToList();
        }
    }
}
