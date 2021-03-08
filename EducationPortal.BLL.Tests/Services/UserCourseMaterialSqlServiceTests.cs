using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.ServicesSql;
using EducationPortal.Domain.Entities;
using Entities;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BLL.Tests.ServicesSql
{
    [TestClass]
    public class UserCourseMaterialSqlServiceTests
    {
        private Mock<IRepository<UserCourseMaterial>> userCourseMaterialRepository;
        private Mock<ICourseMaterialService> courseMaterialService;
        private static Mock<ILogger<UserCourseMaterialService>> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.userCourseMaterialRepository = new Mock<IRepository<UserCourseMaterial>>();
            this.courseMaterialService = new Mock<ICourseMaterialService>();
            logger = new Mock<ILogger<UserCourseMaterialService>>();
        }

        #region AddMaterialsToUserCourse

        [TestMethod]
        public async Task AddMaterialsToUserCourse_MaterialsNotNull_ReturnTrue()
        {
            List<Material> materials = new List<Material>()
            {
                new Material() {Id = 0},
                new Material() {Id = 1},
                new Material() {Id = 2}
            };

            courseMaterialService.Setup(db => db.GetAllMaterialsFromCourse(It.IsAny<int>())).ReturnsAsync(materials);
            userCourseMaterialRepository.Setup(db => db.Add(It.IsAny<UserCourseMaterial>()));
            userCourseMaterialRepository.Setup(db => db.Save());

            UserCourseMaterialService userCourseMaterialSqlService = new UserCourseMaterialService(
                userCourseMaterialRepository.Object, 
                courseMaterialService.Object,
                logger.Object);

            userCourseMaterialSqlService.AddMaterialsToUserCourse(0, 0);

            userCourseMaterialRepository.Verify(x => x.Add(It.IsAny<UserCourseMaterial>()), Times.Exactly(materials.Count));
            userCourseMaterialRepository.Verify(x => x.Save(), Times.Exactly(materials.Count));
            Assert.IsTrue(await userCourseMaterialSqlService.AddMaterialsToUserCourse(0, 0));
        }

        [TestMethod]
        public async Task AddMaterialsToUserCourse_MaterialsNull_False()
        {
            List<Material> materials = null;

            courseMaterialService.Setup(db => db.GetAllMaterialsFromCourse(It.IsAny<int>())).ReturnsAsync(materials);

            UserCourseMaterialService userCourseMaterialSqlService = new UserCourseMaterialService(
                userCourseMaterialRepository.Object,
                courseMaterialService.Object,
                logger.Object);

            Assert.IsFalse(await userCourseMaterialSqlService.AddMaterialsToUserCourse(0, 0));
        }
        #endregion

        #region SetPassToMaterial

        [TestMethod]
        public async Task SetPassToMaterial_UserCourseMaterialExist_True()
        {
            List<UserCourseMaterial> userCourseMaterials = new List<UserCourseMaterial>()
            {
                new UserCourseMaterial() { UserCourseId = 0, MaterialId = 0 }
            };

            userCourseMaterialRepository.Setup(db => db.Get(It.IsAny<Expression<Func<UserCourseMaterial, bool>>>())).ReturnsAsync(userCourseMaterials);
            userCourseMaterialRepository.Setup(db => db.Update(It.IsAny<UserCourseMaterial>()));
            userCourseMaterialRepository.Setup(db => db.Save());

            UserCourseMaterialService userCourseMaterialSqlService = new UserCourseMaterialService(
                userCourseMaterialRepository.Object,
                courseMaterialService.Object,
                logger.Object);

            userCourseMaterialSqlService.SetPassToMaterial(0, 0);

            userCourseMaterialRepository.Verify(x => x.Update(It.IsAny<UserCourseMaterial>()), Times.Once);
            userCourseMaterialRepository.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(await userCourseMaterialSqlService.SetPassToMaterial(0, 0));
        }

        [TestMethod]
        public async Task SetPassToMaterial_UserCourseMaterialNotExist_False()
        {
            List<UserCourseMaterial> userCourseMaterials = new List<UserCourseMaterial>();

            userCourseMaterialRepository.Setup(db => db.Get(It.IsAny<Expression<Func<UserCourseMaterial, bool>>>())).ReturnsAsync(userCourseMaterials);
            userCourseMaterialRepository.Setup(db => db.Update(It.IsAny<UserCourseMaterial>()));
            userCourseMaterialRepository.Setup(db => db.Save());

            UserCourseMaterialService userCourseMaterialSqlService = new UserCourseMaterialService(
                userCourseMaterialRepository.Object,
                courseMaterialService.Object,
                logger.Object);

            Assert.IsFalse(await userCourseMaterialSqlService.SetPassToMaterial(0, 0));
        }

        #endregion

        #region GetNotPassedMaterialsFromCourseInProgress

        [TestMethod]
        public void GetNotPassedMaterialsFromCourseInProgress_UserCourseMaterialNotExist_Null()
        {
            userCourseMaterialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<UserCourseMaterial, bool>>>())).ReturnsAsync(false);

            UserCourseMaterialService userCourseMaterialSqlService = new UserCourseMaterialService(
                userCourseMaterialRepository.Object,
                courseMaterialService.Object,
                logger.Object);

            Assert.IsNull(userCourseMaterialSqlService.GetNotPassedMaterialsFromCourseInProgress(It.IsAny<int>()));
        }

        [TestMethod]
        public void GetNotPassedMaterialsFromCourseInProgress_()
        {
            userCourseMaterialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<UserCourseMaterial, bool>>>())).ReturnsAsync(true);
            userCourseMaterialRepository.Setup(
                db => db.Get<Material>(It.IsAny<Expression<Func<UserCourseMaterial, Material>>>(),
                It.IsAny<Expression<Func<UserCourseMaterial, bool>>>()
                )).ReturnsAsync(new List<Material>());

            UserCourseMaterialService userCourseMaterialSqlService = new UserCourseMaterialService(
                userCourseMaterialRepository.Object,
                courseMaterialService.Object,
                logger.Object);

            userCourseMaterialSqlService.GetNotPassedMaterialsFromCourseInProgress(0);

            userCourseMaterialRepository.Verify(x => x.Get<Material>(It.IsAny<Expression<Func<UserCourseMaterial, Material>>>(),
                It.IsAny<Expression<Func<UserCourseMaterial, bool>>>()), Times.Once);
        }

        #endregion
    }
}
