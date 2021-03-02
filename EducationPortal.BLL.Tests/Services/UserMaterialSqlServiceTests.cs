using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.ServicesSql;
using EducationPortal.Domain.Entities;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EducationPortal.BLL.Tests.ServicesSql
{
    [TestClass]
    public class UserMaterialSqlServiceTests
    {
        private Mock<IRepository<UserMaterial>> userMaterialRepository;
        private Mock<IBLLLogger> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.userMaterialRepository = new Mock<IRepository<UserMaterial>>();
            this.logger = new Mock<IBLLLogger>();
        }

        #region AddMaterialToUser

        [TestMethod]
        public void AddMaterialToUser_UserMaterialExist_False()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            userMaterialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<UserMaterial, bool>>>())).Returns(true);

            UserMaterialService userMaterialSqlService = new UserMaterialService(
                userMaterialRepository.Object,
                logger.Object);

            Assert.IsFalse(userMaterialSqlService.AddMaterialToUser(It.IsAny<int>(), It.IsAny<int>()));
        }

        [TestMethod]
        public void AddMaterialToUser_UserMaterialNotExist_True()
        {
            userMaterialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<UserMaterial, bool>>>())).Returns(false);
            userMaterialRepository.Setup(db => db.Add(It.IsAny<UserMaterial>()));
            userMaterialRepository.Setup(db => db.Save());

            UserMaterialService userMaterialSqlService = new UserMaterialService(
                userMaterialRepository.Object,
                logger.Object);

            userMaterialSqlService.AddMaterialToUser(It.IsAny<int>(), It.IsAny<int>());


            Assert.IsTrue(userMaterialSqlService.AddMaterialToUser(It.IsAny<int>(), It.IsAny<int>()));
        }

        #endregion

        #region GetAllMaterialInUser

        [TestMethod]
        public void GetAllMaterialInUser_CallGet()
        {
            userMaterialRepository.Setup(
                db => db.Get<Material>(It.IsAny<Expression<Func<UserMaterial, Material>>>(),
                It.IsAny<Expression<Func<UserMaterial, bool>>>()
                )).Returns(new List<Material>());

            UserMaterialService userMaterialSqlService = new UserMaterialService(
                userMaterialRepository.Object,
                logger.Object);

            userMaterialSqlService.GetAllMaterialInUser(It.IsAny<int>());

            userMaterialRepository.Verify(x => x.Get<Material>(It.IsAny<Expression<Func<UserMaterial, Material>>>(),
                It.IsAny<Expression<Func<UserMaterial, bool>>>()
                ), Times.Once);
        }

        #endregion
    }
}
