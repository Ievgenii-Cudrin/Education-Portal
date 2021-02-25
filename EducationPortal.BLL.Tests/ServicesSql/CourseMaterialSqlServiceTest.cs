using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EducationPortal.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Interfaces;
using EducationPortal.Domain.Entities;
using EducationPortal.BLL.Services;
using System.Linq.Expressions;
using DataAccessLayer.Entities;
using Entities;

namespace EducationPortal.BLL.Tests.ServicesSql
{
    [TestClass]
    public class CourseMaterialSqlServiceTest
    {
        [TestMethod]

        public void AddMaterialToCourse_CourseMaterialExist_False()
        {
            Mock<IRepository<CourseMaterial>> courseMaterialRepo = new Mock<IRepository<CourseMaterial>>();
            courseMaterialRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseMaterial, bool>>>())).Returns(true);

            CourseMaterialSqlService courseMatService = new CourseMaterialSqlService(courseMaterialRepo.Object);

            Assert.IsFalse(courseMatService.AddMaterialToCourse(2, 3));
        }

        [TestMethod]
        public void AddMaterialToCourse_CourseMaterialNotExist_True()
        {
            Mock<IRepository<CourseMaterial>> courseMaterialRepo = new Mock<IRepository<CourseMaterial>>();
            courseMaterialRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseMaterial, bool>>>())).Returns(false);
            courseMaterialRepo.Setup(db => db.Add(It.IsAny<CourseMaterial>()));
            courseMaterialRepo.Setup(db => db.Save());

            CourseMaterialSqlService courseMatService = new CourseMaterialSqlService(courseMaterialRepo.Object);
            CourseMaterial courseMaterial = new CourseMaterial()
            {
                CourseId = 2,
                MaterialId = 3
            };
            courseMatService.AddMaterialToCourse(2, 3);

            courseMaterialRepo.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(courseMatService.AddMaterialToCourse(2, 3));
        }

        [TestMethod]
        public void GetAllMaterialsFromCourse_ReturnListMaterials()
        {
            Mock<IRepository<CourseMaterial>> courseMaterialRepo = new Mock<IRepository<CourseMaterial>>();
            courseMaterialRepo.Setup(db => db.Get<Material>(It.IsAny<Expression<Func<CourseMaterial, Material>>>(),
                It.IsAny<Expression<Func<CourseMaterial, bool>>>())).Returns(new List<Material>());

            CourseMaterialSqlService courseMaterialSqlService = new CourseMaterialSqlService(courseMaterialRepo.Object);
            courseMaterialSqlService.GetAllMaterialsFromCourse(0);

            courseMaterialRepo.Verify(x => x.Get<Material>(x => x.Material, x => x.CourseId == 0), Times.Once);
        }

    }
}
