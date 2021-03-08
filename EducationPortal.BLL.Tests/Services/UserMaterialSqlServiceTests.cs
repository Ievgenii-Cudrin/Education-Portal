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
    public class UserMaterialSqlServiceTests
    {
        private Mock<IRepository<UserMaterial>> userMaterialRepository;
        private Mock<ILogger<UserMaterialService>> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.userMaterialRepository = new Mock<IRepository<UserMaterial>>();
            this.logger = new Mock<ILogger<UserMaterialService>>();
        }

        #region AddMaterialToUser

        [TestMethod]
        public async Task AddMaterialToUser_UserMaterialExist_False()
        {
            userMaterialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<UserMaterial, bool>>>())).ReturnsAsync(true);

            UserMaterialService userMaterialSqlService = new UserMaterialService(
                userMaterialRepository.Object,
                logger.Object);

            Assert.IsFalse(await userMaterialSqlService.AddMaterialToUser(It.IsAny<int>(), It.IsAny<int>()));
        }

        [TestMethod]
        public async Task AddMaterialToUser_UserMaterialNotExist_True()
        {
            userMaterialRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<UserMaterial, bool>>>())).ReturnsAsync(false);
            userMaterialRepository.Setup(db => db.Add(It.IsAny<UserMaterial>()));
            userMaterialRepository.Setup(db => db.Save());

            UserMaterialService userMaterialSqlService = new UserMaterialService(
                userMaterialRepository.Object,
                logger.Object);

            await userMaterialSqlService.AddMaterialToUser(It.IsAny<int>(), It.IsAny<int>());


            Assert.IsTrue(await userMaterialSqlService.AddMaterialToUser(It.IsAny<int>(), It.IsAny<int>()));
        }

        #endregion

        #region GetAllMaterialInUser

        [TestMethod]
        public void GetAllMaterialInUser_CallGet()
        {
            userMaterialRepository.Setup(
                db => db.Get<Material>(It.IsAny<Expression<Func<UserMaterial, Material>>>(),
                It.IsAny<Expression<Func<UserMaterial, bool>>>()
                )).ReturnsAsync(new List<Material>());

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
