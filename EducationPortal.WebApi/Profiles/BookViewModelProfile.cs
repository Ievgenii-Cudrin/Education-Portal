using AutoMapper;
using EducationPartal.WebApi.ModelsView;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Profiles
{
    public class BookViewModelProfile : Profile
    {
        public BookViewModelProfile()
        {
            CreateMap<BookViewModel, Book>();
        }
    }
}
