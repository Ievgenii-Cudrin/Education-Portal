using AutoMapper;
using EducationPartal.CoreMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPartal.CoreMVC.Mappers
{
    public class Mapping : IAutoMapperService
    {
        public TDomain CreateMapFromVMToDomain<TView, TDomain>(TView viewModelType)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TView, TDomain>());
            var mapper = new Mapper(config);
            var domainAfterMapping = mapper.Map<TView, TDomain>(viewModelType);

            return domainAfterMapping;
        }

        public TDomain CreateMapFromVMToDomainWithIncludeVideoType<TView, TDomain, TViewInclude, TDomainInclude>(TView viewModelType)
            where TViewInclude : TView
            where TDomainInclude : TDomain
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TView, TDomain>()
                    .Include<TViewInclude, TDomainInclude>();
                cfg.CreateMap<TViewInclude, TDomainInclude>();
            });
            var mapper = new Mapper(configuration);
            var domainAfterMapping = mapper.Map<TView, TDomain>(viewModelType);

            return domainAfterMapping;
        }

        public List<Domain> CreateListMap<View, Domain>(List<View> list)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<View, Domain>());
            var mapper = new Mapper(configuration);
            List<Domain> listDest = mapper.Map<List<View>, List<Domain>>(list);

            return listDest;
        }

        public List<TMaterialView> CreateListMapFromVMToDomainWithIncludeMaterialType
            <TMaterialDomainD, TMaterialView, TVideoDomain, TVideoView, TArticleDomain, TArticleView, TBookDomain, TBookView>(List<TMaterialDomainD> viewModelType)
            where TVideoDomain : TMaterialDomainD
            where TArticleDomain : TMaterialDomainD
            where TBookDomain : TMaterialDomainD
            where TVideoView : TMaterialView
            where TArticleView : TMaterialView
            where TBookView : TMaterialView
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialDomainD, TMaterialView>()
                    .Include<TVideoDomain, TVideoView>()
                    .Include<TArticleDomain, TArticleView>()
                    .Include<TBookDomain, TBookView>();
                cfg.CreateMap<TBookDomain, TBookView>();
                cfg.CreateMap<TVideoDomain, TVideoView>();
                cfg.CreateMap<TArticleDomain, TArticleView>();
            });
            var mapper = new Mapper(configuration);
            var domainAfterMapping = mapper.Map<List<TMaterialDomainD>, List<TMaterialView>>(viewModelType);

            return domainAfterMapping;
        }

        public List<TCourseView> CreateListMapFromVMToDomainWithIncludeLsitType
            <TCourseDomain, TCourseView, TMaterialDomain, TMaterialView, TSkillDomain, TSkillView>(List<TCourseDomain> viewModelType)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCourseDomain, TCourseView>();
                cfg.CreateMap<List<TMaterialDomain>, List<TMaterialView>>();
                cfg.CreateMap<List<TSkillDomain>, List<TSkillView>>();
            });

            var mapper = new Mapper(configuration);
            var domainAfterMapping = mapper.Map<List<TCourseDomain>, List<TCourseView>>(viewModelType);

            return domainAfterMapping;
        }

        public TCourseView CreateMapFromVMToDomainWithIncludeLsitType
            <TCourseDomain, TCourseView, TModelDomain, TModelView, TSkillDomain, TSkillView>(TCourseDomain viewModelType)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCourseDomain, TCourseView>();
                cfg.CreateMap<List<TModelDomain>, List<TModelView>>();
                cfg.CreateMap<List<TSkillDomain>, List<TSkillView>>();
            });
            var mapper = new Mapper(configuration);
            var domainAfterMapping = mapper.Map<TCourseDomain, TCourseView>(viewModelType);

            return domainAfterMapping;
        }
    }
}
