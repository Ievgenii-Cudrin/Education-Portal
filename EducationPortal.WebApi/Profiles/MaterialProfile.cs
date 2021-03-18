using AutoMapper;
using EducationPartal.WebApi.ModelsView;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Profiles
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<MaterialViewModel, Material>()
                .IncludeAllDerived();

            CreateMap<VideoViewModel, Video>();
            CreateMap<ArticleViewModel, Article>();
            CreateMap<BookViewModel, Book>();
        }
    }
}
