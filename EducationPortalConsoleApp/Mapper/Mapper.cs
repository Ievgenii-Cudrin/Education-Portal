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

        public static List<N> CreateListMap<T, N>(List<T> list)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<T, N>());
            var mapper = new Mapper(configuration);
            List<N> listDest = mapper.Map<List<T>, List<N>>(list);

            return listDest;
        }

        public static List<MV> CreateMapFromVMToDomainWithIncludeMaterialType<MD, MV, VD, VV, AD, AV, BD, BV>(List<MD> viewModelType) where VD : MD where AD : MD where BD : MD where VV : MV where AV : MV where BV : MV
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<MD, MV>()
                    .Include<VD, VV>()
                    .Include<AD, AV>()
                    .Include<BD, BV>(); 
                cfg.CreateMap<BD, BV>();
                cfg.CreateMap<VD, VV>();
                cfg.CreateMap<AD, AV>();
            });
            var mapper = new Mapper(configuration);
            var domainAfterMapping = mapper.Map<List<MD>, List<MV>>(viewModelType);

            return domainAfterMapping;
        }
    }
}
