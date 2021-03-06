namespace EducationPortal.PL.Interfaces
{
    using System.Collections.Generic;

    public interface IMapperService
    {
        TN CreateMapFromVMToDomain<T, TN>(T viewModelType);

        TN CreateMapFromVMToDomainWithIncludeVideoType<T, TN, TIT, TIN>(T viewModelType)
            where TIT : T
            where TIN : TN;

        List<TN> CreateListMap<T, TN>(List<T> list);

        List<TMV> CreateListMapFromVMToDomainWithIncludeMaterialType<TMD, TMV, TVD, TVV, TAD, TAV, TBD, TBV>(List<TMD> viewModelType)
            where TVD : TMD
            where TAD : TMD
            where TBD : TMD
            where TVV : TMV
            where TAV : TMV
            where TBV : TMV;

        List<TCV> CreateListMapFromVMToDomainWithIncludeLsitType<TCD, TCV, TMD, TMV, TSD, TSV>(List<TCD> viewModelType);

        TCV CreateMapFromVMToDomainWithIncludeLsitType<TCD, TCV, TMD, TMV, TSD, TSV>(TCD viewModelType);
    }
}
