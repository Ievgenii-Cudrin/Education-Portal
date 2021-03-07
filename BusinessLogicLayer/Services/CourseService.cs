using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;
using NLog;

namespace EducationPortal.BLL.ServicesSql
{

    public class CourseService : ICourseService
    {
        private IRepository<Course> courseRepository;
        private ICourseMaterialService courseMaterialService;
        private ICourseSkillService courseSkillService;
        private IMaterialService materialService;
        private ISkillService skillService;

        public CourseService(
            IRepository<Course> courseRepo,
            ICourseMaterialService courseMaterialServ,
            ICourseSkillService courseSkillServ,
            IMaterialService materialService,
            ISkillService skillService)
        {
            this.courseRepository = courseRepo;
            this.courseMaterialService = courseMaterialServ;
            this.courseSkillService = courseSkillServ;
            this.materialService = materialService;
            this.skillService = skillService;
        }

        public bool AddMaterialToCourse(int courseId, Material material)
        {
            try
            {
                return this.courseMaterialService.AddMaterialToCourse(courseId, material.Id);
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool AddSkillToCourse(int courseId, Skill skillToAdd)
        {
            try
            {
                return this.courseSkillService.AddSkillToCourse(courseId, skillToAdd.Id);
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool CreateCourse(Course course)
        {
            bool uniqueCourse = course != null && !this.courseRepository.Exist(x => x.Name == course.Name);

            if (uniqueCourse)
            {
                this.courseRepository.Add(course);
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            try
            {
                this.courseRepository.Delete(id);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public List<Material> GetMaterialsFromCourse(int id)
        {
            try
            {
                return this.courseMaterialService.GetAllMaterialsFromCourse(id);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public List<Skill> GetSkillsFromCourse(int id)
        {
            try
            {
                return this.courseSkillService.GetAllSkillsFromCourse(id);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public bool UpdateCourse(Course course)
        {
            try
            {
                this.courseRepository.Update(course);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool ExistCourse(int courseId)
        {
            return this.courseRepository.Exist(x => x.Id == courseId);
        }

        public List<Course> GetCoursesPerPage(int skip, int take)
        {
            return this.courseRepository.GetPage(skip, take).ToList();
        }

        public int GetCount()
        {
            return this.courseRepository.Count();
        }
    }
}
