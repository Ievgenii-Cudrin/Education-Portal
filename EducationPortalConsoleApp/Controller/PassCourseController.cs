using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using EducationPortal.PL.Interfaces;
using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationPortal.PL.Controller
{
    public class PassCourseController : IPassCourseController
    {
        ICourseService courseService;
        IMaterialController materialController;

        public PassCourseController(ICourseService courseService, IMaterialController materialController)
        {
            this.courseService = courseService;
            this.materialController = materialController;
        }

        public void StartPassCourse()
        {
            List<Course> c = courseService.GetAllCourses().ToList();
            List<CourseViewModel> coursesListVM = Mapping.Mapping.CreateListMapFromVMToDomainWithIncludeLsitType<Course, CourseViewModel, Material, MaterialViewModel, Skill, SkillViewModel>(courseService.GetAllCourses().ToList());

            foreach(var course in coursesListVM)
            {
                course.Materials = Mapping.Mapping.CreateListMapFromVMToDomainWithIncludeMaterialType<Material, MaterialViewModel, Video, VideoViewModel, Article, ArticleViewModel, Book, BookViewModel>(courseService.GetMaterialsFromCourse(course.Id));
                course.Skills = Mapping.Mapping.CreateListMap<Skill, SkillViewModel>(courseService.GetSkillsFromCourse(course.Id));
            }

            foreach(var course in coursesListVM)
            {
                Console.WriteLine($"\n№ {course.Id} - {course.Name}");
                Console.WriteLine("This course includes such materials:");

                foreach(var material in course.Materials)
                {
                    Console.WriteLine(material.ToString());
                }

                Console.WriteLine("After completing this course, you will receive such skills: \n");
                
                foreach(var skill in course.Skills)
                {
                    Console.WriteLine($"№ {skill.Id} - {skill.Name}");
                }
            }
        }
    }
}
