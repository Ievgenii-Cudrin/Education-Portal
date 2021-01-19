using AutoMapper;
using DataAccessLayer.Entities;
using EducationPortal.PL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.Mapping
{
    public static class Mapping
    {
        public static N CreateMapFromVMToDomain<T, N>(T viewModelType)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, N>());
            var mapper = new Mapper(config);
            var domainAfterMapping = mapper.Map<T, N>(viewModelType);

            return domainAfterMapping;
        }

        public static N CreateMapFromVMToDomainWithIncludeVideoType<T, N, IT, IN>(T viewModelType) where IT : T where IN : N
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<T, N>()
                    .Include<IT, IN>();
                cfg.CreateMap<IT, IN>();
            });
            var mapper = new Mapper(configuration);
            var domainAfterMapping = mapper.Map<T, N>(viewModelType);

            return domainAfterMapping;
        }
    }
}
