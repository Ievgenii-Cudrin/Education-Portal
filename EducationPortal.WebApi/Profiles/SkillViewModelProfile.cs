using AutoMapper;
using DataAccessLayer.Entities;
using EducationPartal.WebApi.ModelsView;
using EducationPortal.WebApi.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Profiles
{
    public class SkillViewModelProfile : Profile
    {
        public SkillViewModelProfile()
        {
            CreateMap<SkillViewModel, Skill>();
        }
    }
}
