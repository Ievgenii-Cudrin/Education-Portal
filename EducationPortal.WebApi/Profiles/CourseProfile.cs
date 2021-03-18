using AutoMapper;
using DataAccessLayer.Entities;
using EducationPartal.WebApi.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseViewModel>();
            CreateMap<List<Course>, List<CourseViewModel>>();
        }
    }
}
