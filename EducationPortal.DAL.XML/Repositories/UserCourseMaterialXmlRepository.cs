using EducationPortal.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XmlDataBase.Interfaces;
using XmlDataBase.Serialization;

namespace EducationPortal.DAL.XML.Repositories
{
    public class UserCourseMaterialXmlRepository : RepositoryXml<UserCourseMaterial>
    {
        private readonly IXmlSerializeContext<UserCourseMaterial> context;

        public UserCourseMaterialXmlRepository(IXmlSerializeContext<UserCourseMaterial> context)
            :base(context)
        {
            this.context = context;
        }

        public override async Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<UserCourseMaterial, TResult>> selector, Expression<Func<UserCourseMaterial, bool>> predicat)
        {
            var userMaterials = this.context.XmlSet.GetAll().AsQueryable()
                .Where(predicat).ToList();

            Type type = selector.Body.Type;
            Type listType = typeof(List<>).MakeGenericType(new[] { type });
            Type xmlType = typeof(XmlSet<>).MakeGenericType(new[] { type });
            IList list = (IList)Activator.CreateInstance(listType);
            dynamic xmlSet = Activator.CreateInstance(xmlType);

            if (type.Name == "UserCourse")
            {
                foreach (var courseMaterial in userMaterials)
                {
                    list.Add(xmlSet.Get(courseMaterial.UserCourseId));
                }
            }
            else
            {
                foreach (var courseMaterial in userMaterials)
                {
                    list.Add(xmlSet.Get(courseMaterial.MaterialId));
                }
            }

            return (IEnumerable<TResult>)list;
        }
    }
}
