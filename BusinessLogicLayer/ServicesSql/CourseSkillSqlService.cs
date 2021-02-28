namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;

    public class CourseSkillSqlService : ICourseSkillService
    {
        private readonly IRepository<CourseSkill> courseSkillRepository;
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<Skill> skillRepository;
        private static IBLLLogger logger;

        public CourseSkillSqlService(
            IRepository<CourseSkill> courseSkillRepo,
            IRepository<Skill> skillRepo,
            IRepository<Course> courseRepo,
            IBLLLogger log)
        {
            logger = log;
            this.courseSkillRepository = courseSkillRepo;
            this.courseRepository = courseRepo;
            this.skillRepository = skillRepo;
        }

        public bool AddSkillToCourse(int courseId, int skillId)
        {
            if (this.courseRepository.Exist(x => x.Id == courseId) &&
                this.skillRepository.Exist(x => x.Id == skillId) &&
                !this.courseSkillRepository.Exist(x => x.CourseId == courseId && x.SkillId == skillId))
            {
                CourseSkill courseSkill = new CourseSkill()
                {
                    CourseId = courseId,
                    SkillId = skillId,
                };

                this.courseSkillRepository.Add(courseSkill);
                this.courseSkillRepository.Save();
                return true;
            }
            else
            {
                logger.Logger.Debug("Skill dont add to course - " + DateTime.Now);
                return false;
            }
        }

        public List<Skill> GetAllSkillsFromCourse(int courseId)
        {
            return this.courseSkillRepository.Get<Skill>(x => x.Skill, x => x.CourseId == courseId).ToList();
        }
    }
}
