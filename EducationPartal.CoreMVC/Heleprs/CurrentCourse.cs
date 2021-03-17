using DataAccessLayer.Entities;
using EducationPortal.CoreMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.CoreMVC.Heleprs
{
    public class CurrentCourse : ICurrentCourse
    {
        private static Course currentCourse;

        public static Course CurrentCourseInWork
        {
            get { return currentCourse; }
            set { currentCourse = value; }
        }
    }
}
