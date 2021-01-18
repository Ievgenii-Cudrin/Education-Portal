using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICourseService : IDeleteEntity
    {
        public bool CreateCourse(Course course);

        public bool UpdateCourse(Course course);

        public IEnumerable<Course> GetAllSkills();

        public bool AddVideoToCourse(int id, Video video);

        public bool AddBookToCourse(int id, Book book);

        public bool AddArticleToCourse(int id, Article article);

        public bool AddSkillToCourse(int id, Skill skillToAdd);


    }
}
