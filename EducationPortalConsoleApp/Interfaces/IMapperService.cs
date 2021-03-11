using System.Collections.Generic;

namespace EducationPortal.PL.Interfaces
{
    public interface IMapperService
    {
        TDomain CreateMapFromVMToDomain<TView, TDomain>(TView viewModelType);

        public TDomain CreateMapFromVMToDomainWithIncludeVideoType<TView, TDomain, TViewInclude, TDomainInclude>(TView viewModelType)
            where TViewInclude : TView
            where TDomainInclude : TDomain;

        List<Domain> CreateListMap<View, Domain>(List<View> list);

        List<TMaterialView> CreateListMapFromVMToDomainWithIncludeMaterialType
            <TMaterialDomainD, TMaterialView, TVideoDomain, TVideoView, TArticleDomain, TArticleView, TBookDomain, TBookView>(List<TMaterialDomainD> viewModelType)
            where TVideoDomain : TMaterialDomainD
            where TArticleDomain : TMaterialDomainD
            where TBookDomain : TMaterialDomainD
            where TVideoView : TMaterialView
            where TArticleView : TMaterialView
            where TBookView : TMaterialView;

        List<TCourseView> CreateListMapFromVMToDomainWithIncludeLsitType
            <TCourseDomain, TCourseView, TMaterialDomain, TMaterialView, TSkillDomain, TSkillView>(List<TCourseDomain> viewModelType);

        TCourseView CreateMapFromVMToDomainWithIncludeLsitType
            <TCourseDomain, TCourseView, TModelDomain, TModelView, TSkillDomain, TSkillView>(TCourseDomain viewModelType);
    }
}
