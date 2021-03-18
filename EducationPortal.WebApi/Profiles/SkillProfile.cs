using AutoMapper;
using DataAccessLayer.Entities;
using EducationPartal.WebApi.ModelsView;
using EducationPortal.Domain.Entities;
using EducationPortal.WebApi.ModelsView;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<Skill, SkillViewModel>();

            CreateMap<UserSkill, SkillWithCountViewModel>()
                .IncludeAllDerived();

            CreateMap<Skill, SkillViewModel>();

            CreateMap<SkillWithCountViewModel, UserSkill>()
               .IncludeAllDerived();

            CreateMap<SkillViewModel, Skill>();
        }
    }
}
