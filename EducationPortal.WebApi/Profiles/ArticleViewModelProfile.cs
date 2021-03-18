using AutoMapper;
using EducationPartal.WebApi.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Profiles
{
    public class ArticleViewModelProfile : Profile
    {
        public ArticleViewModelProfile()
        {
            CreateMap<ArticleViewModel, Article>();
        }
    }
}
