using AutoMapper;
using EducationPartal.WebApi.ModelsView;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPortal.WebApi.Profiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, VideoViewModel>();
        }
    }
}
