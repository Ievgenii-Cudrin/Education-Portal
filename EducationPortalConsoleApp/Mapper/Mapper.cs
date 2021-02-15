namespace EducationPortal.PL.Mapping
{
    using System.Collections.Generic;
    using AutoMapper;

    public static class Mapping
    {
        public static TN CreateMapFromVMToDomain<T, TN>(T viewModelType)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, TN>());
            var mapper = new Mapper(config);
            var domainAfterMapping = mapper.Map<T, TN>(viewModelType);

            return domainAfterMapping;
        }

        public static TN CreateMapFromVMToDomainWithIncludeVideoType<T, TN, TIT, TIN>(T viewModelType)
            where TIT : T
            where TIN : TN
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, TN>()
                    .Include<TIT, TIN>();
                cfg.CreateMap<TIT, TIN>();
            });
            var mapper = new Mapper(configuration);
            var domainAfterMapping = mapper.Map<T, TN>(viewModelType);

            return domainAfterMapping;
        }

        public static List<TN> CreateListMap<T, TN>(List<T> list)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<T, TN>());
            var mapper = new Mapper(configuration);
            List<TN> listDest = mapper.Map<List<T>, List<TN>>(list);

            return listDest;
        }

        public static List<TMV> CreateListMapFromVMToDomainWithIncludeMaterialType<TMD, TMV, TVD, TVV, TAD, TAV, TBD, TBV>(List<TMD> viewModelType)
            where TVD : TMD
            where TAD : TMD
            where TBD : TMD
            where TVV : TMV
            where TAV : TMV
            where TBV : TMV
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMD, TMV>()
                    .Include<TVD, TVV>()
                    .Include<TAD, TAV>()
                    .Include<TBD, TBV>();
                cfg.CreateMap<TBD, TBV>();
                cfg.CreateMap<TVD, TVV>();
                cfg.CreateMap<TAD, TAV>();
            });
            var mapper = new Mapper(configuration);
            var domainAfterMapping = mapper.Map<List<TMD>, List<TMV>>(viewModelType);

            return domainAfterMapping;
        }

        public static List<TCV> CreateListMapFromVMToDomainWithIncludeLsitType<TCD, TCV, TMD, TMV, TSD, TSV>(List<TCD> viewModelType)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCD, TCV>();
                cfg.CreateMap<List<TMD>, List<TMV>>();
                cfg.CreateMap<List<TSD>, List<TSV>>();
            });

            var mapper = new Mapper(configuration);
            var domainAfterMapping = mapper.Map<List<TCD>, List<TCV>>(viewModelType);

            return domainAfterMapping;
        }

        public static TCV CreateMapFromVMToDomainWithIncludeLsitType<TCD, TCV, TMD, TMV, TSD, TSV>(TCD viewModelType)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCD, TCV>();
                cfg.CreateMap<List<TMD>, List<TMV>>();
                cfg.CreateMap<List<TSD>, List<TSV>>();
            });
            var mapper = new Mapper(configuration);
            var domainAfterMapping = mapper.Map<TCD, TCV>(viewModelType);

            return domainAfterMapping;
        }
    }
}
