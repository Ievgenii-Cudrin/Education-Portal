using AutoMapper;
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
    }
}
