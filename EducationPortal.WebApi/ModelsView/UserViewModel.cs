﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPartal.WebApi.ModelsView
{
    public class UserViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<CourseViewModel> Skills { get; set; }

        public List<CourseViewModel> CoursesInProgress { get; set; }

        public List<CourseViewModel> CoursesPassed { get; set; }
    }
}
