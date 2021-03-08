using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.ServicesSql;
using Entities;
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
    public class MaterialSqlServiceTests
    {
        private Mock<IRepository<Material>> materialRepository;
        private Mock<IUserMaterialSqlService> userMaterialService;
        private Mock<ICourseMaterialService> courseMaterialService;
        private Mock<IAuthorizedUser> authorizedUser;
        private Mock<ILogger> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.materialRepository = new Mock<IRepository<Material>>();
            this.userMaterialService = new Mock<IUserMaterialSqlService>();
            this.courseMaterialService = new Mock<ICourseMaterialService>();
            this.authorizedUser = new Mock<IAuthorizedUser>();
            this.logger = new Mock<ILogger>();
        }

        #region CreateMaterial

        [TestMethod]
        public void CreateMaterial_MaterialNull_Null()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).ReturnsAsync(false);

            MaterialService materialSqlService = new MaterialService(
                materialRepository.Object,
                userMaterialService.Object, 
                authorizedUser.Object, 
                courseMaterialService.Object);

            Material material = null;

            Assert.IsNull(materialSqlService.CreateMaterial(material));
        }

        [TestMethod]
        public void CreateMaterial_MaterialExist_Null()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).ReturnsAsync(true);

            MaterialService materialSqlService = new MaterialService(
                materialRepository.Object,
                userMaterialService.Object,
                authorizedUser.Object,
                courseMaterialService.Object);

            Material material = new Material();

            Assert.IsNull(materialSqlService.CreateMaterial(material));
        }

        [TestMethod]
        public void CreateMaterial_MaterialNotNullNoetExist_Null()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).ReturnsAsync(false);
            materialRepository.Setup(db => db.Add(It.IsAny<Material>()));
            materialRepository.Setup(db => db.Save());

            MaterialService materialSqlService = new MaterialService(
                materialRepository.Object,
                userMaterialService.Object,
                authorizedUser.Object,
                courseMaterialService.Object);

            Material material = new Material();
            materialSqlService.CreateMaterial(material);

            materialRepository.Verify(x => x.Add(material), Times.Once);
            materialRepository.Verify(x => x.Save(), Times.Once);
        }

        #endregion

        #region Delete

        [TestMethod]
        public async Task Delete_MaterialNotExist_False()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).ReturnsAsync(false);

            MaterialService materialSqlService = new MaterialService(
                materialRepository.Object,
                userMaterialService.Object,
                authorizedUser.Object,
                courseMaterialService.Object);

            Assert.IsFalse(await materialSqlService.Delete(0));
        }

        [TestMethod]
        public async Task Delete_MaterialExist_True()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).ReturnsAsync(true);
            materialRepository.Setup(db => db.Delete(It.IsAny<int>()));
            materialRepository.Setup(db => db.Save());

            MaterialService materialSqlService = new MaterialService(
                materialRepository.Object,
                userMaterialService.Object,
                authorizedUser.Object,
                courseMaterialService.Object);

            materialSqlService.Delete(0);

            materialRepository.Verify(x => x.Delete(0), Times.Once);
            materialRepository.Verify(x => x.Save());
            Assert.IsTrue(await materialSqlService.Delete(0));
        }

        #endregion


        #region GetMaterial

        [TestMethod]
        public void GetMaterial_MaterialExist_Null()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).ReturnsAsync(false);

            MaterialService materialSqlService = new MaterialService(
                materialRepository.Object,
                userMaterialService.Object,
                authorizedUser.Object,
                courseMaterialService.Object);

            Assert.IsNull(materialSqlService.GetMaterial(0));
        }

        [TestMethod]
        public void GetMaterial_MaterialNotExist_CallGet()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).ReturnsAsync(true);
            materialRepository.Setup(db => db.Get(It.IsAny<Expression<Func<Material, bool>>>())).ReturnsAsync(new List<Material>());

            MaterialService materialSqlService = new MaterialService(
                materialRepository.Object,
                userMaterialService.Object,
                authorizedUser.Object,
                courseMaterialService.Object);

            materialSqlService.GetMaterial(0);

            materialRepository.Verify(x => x.Get(x => x.Id == 0), Times.Once);
        }

        #endregion

        #region ExistMaterial

        [TestMethod]
        public void ExistMaterial_CallExistMethod()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).ReturnsAsync(true);

            MaterialService materialSqlService = new MaterialService(
                materialRepository.Object,
                userMaterialService.Object,
                authorizedUser.Object,
                courseMaterialService.Object);

            materialSqlService.ExistMaterial(0);

            materialRepository.Verify(x => x.Exist(x => x.Id == 0), Times.Once);
        }

        #endregion
    }
}
