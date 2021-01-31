using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.Models
{
    public class UserViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<SkillViewModel> Skills = new List<SkillViewModel>();

        public List<CourseViewModel> CoursesInProgress = new List<CourseViewModel>();

        public List<CourseViewModel> CoursesPassed = new List<CourseViewModel>();
    }
}
