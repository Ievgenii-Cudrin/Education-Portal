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
    public class UserCourseXmlRepository : RepositoryXml<UserCourse>
    {
        private readonly IXmlSerializeContext<UserCourse> context;

        public UserCourseXmlRepository(IXmlSerializeContext<UserCourse> context)
            :base(context)
        {
            this.context = context;
        }

        public override async Task<IList<TResult>> Get<TResult>(Expression<Func<UserCourse, TResult>> selector, Expression<Func<UserCourse, bool>> predicat)
        {
            var userMaterials = this.context.XmlSet.GetAll().AsQueryable()
                .Where(predicat).ToList();

            Type type = selector.Body.Type;
            Type listType = typeof(List<>).MakeGenericType(new[] { type });
            Type xmlType = typeof(XmlSet<>).MakeGenericType(new[] { type });
            IList list = (IList)Activator.CreateInstance(listType);
            dynamic xmlSet = Activator.CreateInstance(xmlType);

            if (type.Name == "Course")
            {
                foreach (var courseMaterial in userMaterials)
                {
                    list.Add(xmlSet.Get(courseMaterial.CourseId));
                }
            }
            else
            {
                foreach (var courseMaterial in userMaterials)
                {
                    list.Add(xmlSet.Get(courseMaterial.UserId));
                }
            }

            return (IList<TResult>)list;
        }
    }
}
