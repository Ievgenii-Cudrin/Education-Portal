using AutoMapper;
using DataAccessLayer.Entities;
using EducationPartal.WebApi.ModelsView;
using EducationPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Profiles
{
    public class CourseViewModelProfile : Profile
    {
        public CourseViewModelProfile()
        {
            CreateMap<CourseViewModel, Course>();
            CreateMap<List<CourseViewModel>, List<Course>>();
            CreateMap<List<CourseViewModel>, List<CourseDTO>>();
            CreateMap<List<CourseDTO>, List<CourseViewModel>>();
        }
    }
}
