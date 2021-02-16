using System.Collections.Generic;

namespace EducationPortal.PL.Interfaces
{
    public interface IMapperService
    {
        public TN CreateMapFromVMToDomain<T, TN>(T viewModelType);

        public TN CreateMapFromVMToDomainWithIncludeVideoType<T, TN, TIT, TIN>(T viewModelType)
            where TIT : T
            where TIN : TN;

        public List<TN> CreateListMap<T, TN>(List<T> list);

        public List<TMV> CreateListMapFromVMToDomainWithIncludeMaterialType<TMD, TMV, TVD, TVV, TAD, TAV, TBD, TBV>(List<TMD> viewModelType)
            where TVD : TMD
            where TAD : TMD
            where TBD : TMD
            where TVV : TMV
            where TAV : TMV
            where TBV : TMV;

        public List<TCV> CreateListMapFromVMToDomainWithIncludeLsitType<TCD, TCV, TMD, TMV, TSD, TSV>(List<TCD> viewModelType);

        public TCV CreateMapFromVMToDomainWithIncludeLsitType<TCD, TCV, TMD, TMV, TSD, TSV>(TCD viewModelType);


    }
}
