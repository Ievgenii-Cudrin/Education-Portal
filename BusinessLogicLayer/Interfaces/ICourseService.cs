using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICourseService : IDeleteEntity
    {
        public bool CreateCourse(string name, string description);

        public bool UpdateCourse(int id, string name, string description);

        public IEnumerable<string> GetAllSkills();


    }
}
