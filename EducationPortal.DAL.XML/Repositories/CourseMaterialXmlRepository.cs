using DataAccessLayer.Interfaces;
using EducationPortal.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XmlDataBase.Interfaces;
using XmlDataBase.Serialization;

namespace EducationPortal.DAL.XML.Repositories
{
    public class CourseMaterialXmlRepository : RepositoryXml<CourseMaterial>
    {
        private readonly IXmlSerializeContext<CourseMaterial> context;

        public CourseMaterialXmlRepository(IXmlSerializeContext<CourseMaterial> context)
            :base(context)
        {
            this.context = context;
        }

        public override async Task<IList<TResult>> Get<TResult>(
            Expression<Func<CourseMaterial, TResult>> selector,
            Expression<Func<CourseMaterial, bool>> predicat)
        {
            var courseMaterials = this.context.XmlSet.GetAll().AsQueryable()
                .Where(predicat).ToList();

            Type type = selector.Body.Type;
            Type listType = typeof(List<>).MakeGenericType(new[] { type });
            Type xmlType = typeof(XmlSet<>).MakeGenericType(new[] { type });
            IList list = (IList)Activator.CreateInstance(listType);
            dynamic xmlSet = Activator.CreateInstance(xmlType);

            if (type.Name == "Material")
            {
                foreach (var courseMaterial in courseMaterials)
                {
                    list.Add(xmlSet.Get(courseMaterial.MaterialId));
                }
            }
            else
            {
                foreach (var courseMaterial in courseMaterials)
                {
                    list.Add(xmlSet.Get(courseMaterial.CourseId));
                }
            }

            return (IList<TResult>)list;
        }
    }
}
