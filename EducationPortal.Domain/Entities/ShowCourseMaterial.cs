﻿namespace EducationPortal.Domain.Entities
{
    using global::Entities;

    public class ShowCourseMaterial
    {
        public int UserCourseId { get; set; }

        public UserCourse UserCourse { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }
    }
}
