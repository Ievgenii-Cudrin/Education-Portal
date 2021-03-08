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
using EducationPortal.BLL.Interfaces;
using NLog;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EducationPortal.BLL.Tests.ServicesSql
{
    [TestClass]
    public class CourseMaterialSqlServiceTest
    {
        Mock<IAuthorizedUser> user;
        Mock<IRepository<CourseMaterial>> courseMaterialRepo;
        Mock<ILogger<CourseMaterialService>> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.user = new Mock<IAuthorizedUser>();
            this.courseMaterialRepo = new Mock<IRepository<CourseMaterial>>();
            this.logger = new Mock<ILogger<CourseMaterialService>>();
        }

        [TestMethod]
        public async Task AddMaterialToCourse_CourseMaterialNotExist_True()
        {
            courseMaterialRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseMaterial, bool>>>())).ReturnsAsync(false);
            courseMaterialRepo.Setup(db => db.Add(It.IsAny<CourseMaterial>()));
            courseMaterialRepo.Setup(db => db.Save());

            CourseMaterialService courseMatService = new CourseMaterialService(
                courseMaterialRepo.Object,
                logger.Object,
                user.Object);

            CourseMaterial courseMaterial = new CourseMaterial()
            {
                CourseId = 2,
                MaterialId = 3
            };
            await courseMatService.AddMaterialToCourse(2, 3);

            courseMaterialRepo.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(await courseMatService.AddMaterialToCourse(2, 3));
        }
    }
}
