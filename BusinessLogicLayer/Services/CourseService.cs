using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using Entities;

namespace EducationPortal.BLL.ServicesSql
{

    public class CourseService : ICourseService
    {
        private IRepository<Course> courseRepository;
        private ICourseMaterialService courseMaterialService;
        private ICourseSkillService courseSkillService;
        private IMaterialService materialService;
        private ISkillService skillService;
        private ICourseComparerService courseComparerService;
        private static IBLLLogger logger;


        public CourseService(
            IRepository<Course> courseRepo,
            ICourseMaterialService courseMaterialServ,
            ICourseSkillService courseSkillServ,
            IMaterialService materialService,
            ISkillService skillService,
            ICourseComparerService courseComparerService,
            IBLLLogger log)
        {
            this.courseRepository = courseRepo;
            this.courseMaterialService = courseMaterialServ;
            this.courseSkillService = courseSkillServ;
            this.materialService = materialService;
            this.skillService = skillService;
            this.courseComparerService = courseComparerService;
            logger = log;
        }

        public bool AddMaterialToCourse(int courseId, Material material)
        {
            if (this.courseRepository.Exist(x => x.Id == courseId) &&
                this.materialService.ExistMaterial(material.Id))
            {
                return this.courseMaterialService.AddMaterialToCourse(courseId, material.Id);
            }

            logger.Logger.Debug("Material dont add to course - " + DateTime.Now);
            return false;
        }

        public bool AddSkillToCourse(int courseId, Skill skillToAdd)
        {
            return this.courseSkillService.AddSkillToCourse(courseId, skillToAdd.Id);
        }

        public bool CreateCourse(Course course)
        {
            bool uniqueCourse = course != null && !this.courseRepository.Exist(x => x.Name == course.Name);

            if (uniqueCourse)
            {
                this.courseRepository.Add(course);
                this.courseRepository.Save();
                return true;
            }

            logger.Logger.Debug("Course not created - " + DateTime.Now);
            return false;
        }

        public bool Delete(int id)
        {
            if (this.courseRepository.Exist(x => x.Id == id))
            {
                this.courseRepository.Delete(id);
                this.courseRepository.Save();
                return true;
            }

            logger.Logger.Debug("Course not deleted - " + DateTime.Now);
            return false;
        }

        public List<Material> GetMaterialsFromCourse(int id)
        {
            if (this.courseRepository.Exist(x => x.Id == id))
            {
                return this.courseMaterialService.GetAllMaterialsFromCourse(id);
            }

            logger.Logger.Debug("Return null materials, course not exist - " + DateTime.Now);
            return null;
        }

        public List<Skill> GetSkillsFromCourse(int id)
        {
            if (this.courseRepository.Exist(x => x.Id == id))
            {
                return this.courseSkillService.GetAllSkillsFromCourse(id);
            }

            logger.Logger.Debug("Return null skills, course not exist - " + DateTime.Now);
            return null;
        }

        public bool UpdateCourse(Course course)
        {
            if (this.courseRepository.Exist(x => x.Id == course.Id))
            {
                this.courseRepository.Update(course);
                this.courseRepository.Save();
                return true;
            }

            logger.Logger.Debug("Course not updated, course not exist - " + DateTime.Now);
            return false;
        }

        public bool ExistCourse(int courseId)
        {
            return this.courseRepository.Exist(x => x.Id == courseId);
        }

        public List<Course> AvailableCourses(List<Course> courses)
        {
            if (courses != null)
            {
                return this.courseRepository.Except(courses, this.courseComparerService.CourseComparer).ToList();
            }

            logger.Logger.Debug("Not available courses, course list is null - " + DateTime.Now);
            return null;
        }
    }
}
