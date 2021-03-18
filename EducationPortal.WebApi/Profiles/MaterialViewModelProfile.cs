using AutoMapper;
using EducationPartal.WebApi.ModelsView;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Profiles
{
    public class MaterialViewModelProfile : Profile
    {
        public MaterialViewModelProfile()
        {
            CreateMap<Material, MaterialViewModel>()
                .IncludeAllDerived();

            CreateMap<Video, VideoViewModel>();
            CreateMap<Article, ArticleViewModel>();
            CreateMap<Book, BookViewModel>();
        }
    }
}
