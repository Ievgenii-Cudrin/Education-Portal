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

        public CourseSkillSqlService(
            IEnumerable<IRepository<CourseSkill>> courseSkillRepo,
            IEnumerable<IRepository<Skill>> skillRepo,
            IEnumerable<IRepository<Course>> courseRepo
            )
        {
            this.courseSkillRepository = courseSkillRepo.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<CourseSkill>));
            this.courseRepository = courseRepo.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<Course>));
            this.skillRepository = skillRepo.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<Skill>));
        }

        public bool AddSkillToCourse(int courseId, int skillId)
        {
            if (this.courseRepository.Exist(x => x.Id == courseId) && this.skillRepository.Exist(x => x.Id == skillId))
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
                return false;
            }
        }
    }
}
