using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.CoreMVC.Interfaces
{
    public interface ICurrentCourse
    {
        public static Course CurrentCourseInWork { get; set; }
    }
}
