using DataAccessLayer.Interfaces;
using EducationPortal.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        public override IList<TResult> Get<TResult>(Expression<Func<UserCourseMaterial, TResult>> selector, Expression<Func<UserCourseMaterial, bool>> predicat)
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

            return (IList<TResult>)list;
        }

        //public IList<TResult> Get<TResult>(Expression<Func<UserCourseMaterial, TResult>> selector)
        //{
        //    return this.context.XmlSet.GetAll()
        //        .AsQueryable()
        //        .Select(selector).ToList();
        //}

        //public IList<UserCourseMaterial> Get(Expression<Func<UserCourseMaterial, bool>> predicat)
        //{
        //    return this.context.XmlSet.GetAll()
        //        .AsQueryable()
        //        .Where(predicat).ToList();
        //}

        //public IList<UserCourseMaterial> GetAll()
        //{
        //    return this.context.XmlSet.GetAll().ToList();
        //}

        //public IList<UserCourseMaterial> GetAll(params Expression<Func<UserCourseMaterial, object>>[] includes)
        //{
        //    return this.GetAll();
        //}

        //public UserCourseMaterial GetLastEntity<TOrderBy>(Expression<Func<UserCourseMaterial, TOrderBy>> orderBy)
        //{
        //    return this.context.XmlSet.GetAll().AsQueryable().OrderBy(orderBy).Last();
        //}

        //public IList<UserCourseMaterial> GetPage(Expression<Func<UserCourseMaterial, bool>> predicat, PageInfo page)
        //{
        //    var skip = page.Size * (page.Number - 1);
        //    return this.context.XmlSet.GetAll()
        //        .AsQueryable()
        //        .Where(predicat)
        //        .Skip(skip)
        //        .Take(page.Size).ToList();
        //}

        //public IList<UserCourseMaterial> GetPage(PageInfo page)
        //{
        //    var skip = page.Size * (page.Number - 1);
        //    return this.context.XmlSet.GetAll()
        //        .Skip(skip)
        //        .Take(page.Size).ToList();
        //}

        //public void Save()
        //{
        //    //Save document
        //}

        //public void Update(UserCourseMaterial item)
        //{
        //    this.context.XmlSet.Update(item);
        //}
    }
}
