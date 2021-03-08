using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> AddMaterialToCourse(int courseId, Material material)
        {
            try
            {
                return await this.courseMaterialService.AddMaterialToCourse(courseId, material.Id);
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<bool> AddSkillToCourse(int courseId, Skill skillToAdd)
        {
            try
            {
                return await this.courseSkillService.AddSkillToCourse(courseId, skillToAdd.Id);
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<bool> CreateCourse(Course course)
        {
            bool courseExist = await this.courseRepository.Exist(x => x.Name == course.Name);

            if (course != null && !courseExist)
            {
                await this.courseRepository.Add(course);
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await this.courseRepository.Delete(id);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<IList<Material>> GetMaterialsFromCourse(int id)
        {
            try
            {
                return await this.courseMaterialService.GetAllMaterialsFromCourse(id);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task<IList<Skill>> GetSkillsFromCourse(int id)
        {
            try
            {
                return await this.courseSkillService.GetAllSkillsFromCourse(id);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            try
            {
                await this.courseRepository.Update(course);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<bool> ExistCourse(int courseId)
        {
            return await this.courseRepository.Exist(x => x.Id == courseId);
        }

        public async Task<IList<Course>> GetCoursesPerPage(int skip, int take)
        {
            return await this.courseRepository.GetPage(skip, take);
        }

        public async Task<int> GetCount()
        {
            return await this.courseRepository.Count();
        }
    }
}
