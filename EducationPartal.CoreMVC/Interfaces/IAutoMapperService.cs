using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPartal.CoreMVC.Interfaces
{
    public interface IAutoMapperService
    {
        TViewModel CreateMapFromVMToDomain<TDomain, TViewModel>(TDomain viewModelType);

        TViewModel CreateMapFromVMToDomainWithIncludeVideoType<TDomain, TViewModel, TIncludeDomain, TIncludeViewModel>(TDomain viewModelType)
            where TIncludeDomain : TDomain
            where TIncludeViewModel : TViewModel;

        List<TViewModel> CreateListMap<TDomain, TViewModel>(List<TDomain> list);

        TMaterialView CreateOneMapFromVMToDomainWithIncludeMaterialType
            <TMaterialDomainD, TMaterialView, TVideoDomain, TVideoView, TArticleDomain, TArticleView, TBookDomain, TBookView>(TMaterialDomainD viewModelType)
            where TVideoDomain : TMaterialDomainD
            where TArticleDomain : TMaterialDomainD
            where TBookDomain : TMaterialDomainD
            where TVideoView : TMaterialView
            where TArticleView : TMaterialView
            where TBookView : TMaterialView;

        List<TMaterialView> CreateListMapFromVMToDomainWithIncludeMaterialType
            <TMaterialDomain, TMaterialView, TVideoDomain, TVideoView, TArticleDomain, TArticleView, TBookDomain, TBookView>(List<TMaterialDomain> viewModelType)
            where TVideoDomain : TMaterialDomain
            where TArticleDomain : TMaterialDomain
            where TBookDomain : TMaterialDomain
            where TVideoView : TMaterialView
            where TArticleView : TMaterialView
            where TBookView : TMaterialView;

        List<TCourseView> CreateListMapFromVMToDomainWithIncludeLsitType
            <TCourseDomain, TCourseView, TMaterialDomain, TMaterialView, TSkillDomain, TSkillView>(List<TCourseDomain> viewModelType);

        TCourseView CreateMapFromVMToDomainWithIncludeLsitType
            <TCourseDomain, TCourseView, TMaterialDomain, TMaterialView, TSkillDomain, TSkillView>(TCourseDomain viewModelType);

        List<TCourseView> CreateSkillListMapFromVMToDomainWithIncludeSkillType
            <TCourseDomain, TCourseView, TMaterialDomain, TMaterialView>(List<TCourseDomain> viewModelType);
    }
}
