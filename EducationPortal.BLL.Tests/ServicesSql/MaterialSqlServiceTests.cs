using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.ServicesSql;
using EducationPortal.Domain.Comparers;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EducationPortal.BLL.Tests.ServicesSql
{
    [TestClass]
    public class MaterialSqlServiceTests
    {
        private Mock<IRepository<Material>> materialRepository;
        private Mock<IUserMaterialSqlService> userMaterialService;
        private Mock<ICourseMaterialService> courseMaterialService;
        private Mock<IAuthorizedUser> authorizedUser;
        private Mock<IMaterialComparerService> materialComparer;

        [TestInitialize]
        public void SetUp()
        {
            this.materialRepository = new Mock<IRepository<Material>>();
            this.userMaterialService = new Mock<IUserMaterialSqlService>();
            this.courseMaterialService = new Mock<ICourseMaterialService>();
            this.authorizedUser = new Mock<IAuthorizedUser>();
            this.materialComparer = new Mock<IMaterialComparerService>();
        }

        #region CreateMaterial

        [TestMethod]
        public void CreateMaterial_MaterialNull_Null()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).Returns(false);

            MaterialSqlService materialSqlService = new MaterialSqlService(materialRepository.Object,
                userMaterialService.Object, authorizedUser.Object, courseMaterialService.Object, materialComparer.Object);

            Material material = null;

            Assert.IsNull(materialSqlService.CreateMaterial(material));
        }

        [TestMethod]
        public void CreateMaterial_MaterialExist_Null()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).Returns(true);

            MaterialSqlService materialSqlService = new MaterialSqlService(materialRepository.Object,
                userMaterialService.Object, authorizedUser.Object, courseMaterialService.Object, materialComparer.Object);

            Material material = new Material();

            Assert.IsNull(materialSqlService.CreateMaterial(material));
        }

        [TestMethod]
        public void CreateMaterial_MaterialNotNullNoetExist_Null()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).Returns(false);
            materialRepository.Setup(db => db.Add(It.IsAny<Material>()));
            materialRepository.Setup(db => db.Save());

            MaterialSqlService materialSqlService = new MaterialSqlService(materialRepository.Object,
                userMaterialService.Object, authorizedUser.Object, courseMaterialService.Object, materialComparer.Object);

            Material material = new Material();
            materialSqlService.CreateMaterial(material);

            materialRepository.Verify(x => x.Add(material), Times.Once);
            materialRepository.Verify(x => x.Save(), Times.Once);
        }

        #endregion

        #region Delete

        [TestMethod]
        public void Delete_MaterialNotExist_False()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).Returns(false);

            MaterialSqlService materialSqlService = new MaterialSqlService(materialRepository.Object,
                userMaterialService.Object, authorizedUser.Object, courseMaterialService.Object, materialComparer.Object);

            Assert.IsFalse(materialSqlService.Delete(0));
        }

        [TestMethod]
        public void Delete_MaterialExist_True()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).Returns(true);
            materialRepository.Setup(db => db.Delete(It.IsAny<int>()));
            materialRepository.Setup(db => db.Save());

            MaterialSqlService materialSqlService = new MaterialSqlService(materialRepository.Object,
                userMaterialService.Object, authorizedUser.Object, courseMaterialService.Object, materialComparer.Object);

            materialSqlService.Delete(0);

            materialRepository.Verify(x => x.Delete(0), Times.Once);
            materialRepository.Verify(x => x.Save());
            Assert.IsTrue(materialSqlService.Delete(0));
        }

        #endregion

        #region GetAllExceptedMaterials

        [TestMethod]
        public void GetAllExceptedMaterials_ReturnMaterials()
        {
            materialRepository.Setup(db => db.Except(It.IsAny<List<Material>>(), It.IsAny<MaterialComparer>())).Returns(new List<Material>());
            courseMaterialService.Setup(db => db.GetAllMaterialsFromCourse(It.IsAny<int>())).Returns(new List<Material>());
            materialComparer.Setup(db => db.MaterialComparer);

            MaterialSqlService materialSqlService = new MaterialSqlService(materialRepository.Object,
                userMaterialService.Object, authorizedUser.Object, courseMaterialService.Object, materialComparer.Object);

            materialSqlService.GetAllExceptedMaterials(0);

            materialComparer.Verify(x => x.MaterialComparer, Times.Once);
            courseMaterialService.Verify(x => x.GetAllMaterialsFromCourse(0), Times.Once);
            materialRepository.Verify(x => x.Except(
                courseMaterialService.Object.GetAllMaterialsFromCourse(0), materialComparer.Object.MaterialComparer), Times.Once);
        }

        #endregion

        #region GetMaterial

        [TestMethod]
        public void GetMaterial_MaterialExist_Null()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).Returns(false);

            MaterialSqlService materialSqlService = new MaterialSqlService(materialRepository.Object,
                userMaterialService.Object, authorizedUser.Object, courseMaterialService.Object, materialComparer.Object);

            Assert.IsNull(materialSqlService.GetMaterial(0));
        }

        [TestMethod]
        public void GetMaterial_MaterialNotExist_CallGet()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).Returns(true);
            materialRepository.Setup(db => db.Get(It.IsAny<Expression<Func<Material, bool>>>())).Returns(new List<Material>());

            MaterialSqlService materialSqlService = new MaterialSqlService(materialRepository.Object,
                userMaterialService.Object, authorizedUser.Object, courseMaterialService.Object, materialComparer.Object);

            materialSqlService.GetMaterial(0);

            materialRepository.Verify(x => x.Get(x => x.Id == 0), Times.Once);
        }

        #endregion

        #region ExistMaterial

        [TestMethod]
        public void ExistMaterial_CallExistMethod()
        {
            materialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Material, bool>>>())).Returns(true);

            MaterialSqlService materialSqlService = new MaterialSqlService(materialRepository.Object,
                userMaterialService.Object, authorizedUser.Object, courseMaterialService.Object, materialComparer.Object);

            materialSqlService.ExistMaterial(0);

            materialRepository.Verify(x => x.Exist(x => x.Id == 0), Times.Once);
        }

        #endregion
    }
}
