using DataAccessLayer.Entities;
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
    public class UserCourseSqlServiceTests
    {
        private Mock<IRepository<UserCourse>> userCourseRepository;
        private Mock<IUserCourseMaterialSqlService> userCourseMaterialSqlService;
        private Mock<IBLLLogger> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.userCourseRepository = new Mock<IRepository<UserCourse>>();
            this.userCourseMaterialSqlService = new Mock<IUserCourseMaterialSqlService>();
            this.logger = new Mock<IBLLLogger>();
        }

        #region AddCourseToUser

        [TestMethod]
        public void AddCourseToUser_UserCourseExist_CallAllMetods()
        {
            userCourseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<UserCourse, bool>>>())).Returns(true);
            userCourseRepository.Setup(db => db.GetLastEntity(x => x.Id)).Returns(new UserCourse());
            userCourseRepository.Setup(db => db.Add(It.IsAny<UserCourse>()));
            userCourseRepository.Setup(db => db.Save());
            userCourseMaterialSqlService.Setup(db => db.AddMaterialsToUserCourse(It.IsAny<int>(), It.IsAny<int>()));

            UserCourseSqlService userCourseSqlService = new UserCourseSqlService(
                userCourseRepository.Object,
                userCourseMaterialSqlService.Object,
                logger.Object);

            userCourseSqlService.AddCourseToUser(0, 0);

            userCourseRepository.Verify(x => x.GetLastEntity(x => x.Id), Times.Once);
            userCourseRepository.Verify(x => x.Add(It.IsAny<UserCourse>()), Times.Once);
            userCourseRepository.Verify(x => x.Save());
            userCourseMaterialSqlService.Verify(x => x.AddMaterialsToUserCourse(It.IsAny<int>(), It.IsAny<int>()));
        }


        [TestMethod]
        public void AddCourseToUser_UserCourseNotExist_NotCallGetLastEntity()
        {
            userCourseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<UserCourse, bool>>>())).Returns(false);
            userCourseRepository.Setup(db => db.GetLastEntity(x => x.Id)).Returns(new UserCourse());
            userCourseRepository.Setup(db => db.Add(It.IsAny<UserCourse>()));
            userCourseRepository.Setup(db => db.Save());
            userCourseMaterialSqlService.Setup(db => db.AddMaterialsToUserCourse(It.IsAny<int>(), It.IsAny<int>()));

            UserCourseSqlService userCourseSqlService = new UserCourseSqlService(
                userCourseRepository.Object,
                userCourseMaterialSqlService.Object,
                logger.Object);

            userCourseSqlService.AddCourseToUser(0, 0);

            userCourseRepository.Verify(x => x.GetLastEntity(x => x.Id), Times.Never);
            userCourseRepository.Verify(x => x.Add(It.IsAny<UserCourse>()), Times.Once);
            userCourseRepository.Verify(x => x.Save());
            userCourseMaterialSqlService.Verify(x => x.AddMaterialsToUserCourse(It.IsAny<int>(), It.IsAny<int>()));
        }

        #endregion

        #region GetAllCourseInProgress

        [TestMethod]
        public void GetAllCourseInProgress_CallGetMethod()
        {
            userCourseRepository.Setup(
                db => db.Get<Course>(It.IsAny<Expression<Func<UserCourse, Course>>>(),
                It.IsAny<Expression<Func<UserCourse, bool>>>()
                )).Returns(new List<Course>());

            UserCourseSqlService userCourseSqlService = new UserCourseSqlService(
                userCourseRepository.Object,
                userCourseMaterialSqlService.Object,
                logger.Object);

            userCourseSqlService.GetAllCourseInProgress(It.IsAny<int>());

            userCourseRepository.Verify(x => x.Get<Course>(It.IsAny<Expression<Func<UserCourse, Course>>>(),
                It.IsAny<Expression<Func<UserCourse, bool>>>()), Times.Once);
        }

        #endregion

        #region ExistUserCourse

        [TestMethod]
        public void ExistUserCourse_CallExist()
        {
            userCourseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<UserCourse, bool>>>()));

            UserCourseSqlService userCourseSqlService = new UserCourseSqlService(
                userCourseRepository.Object,
                userCourseMaterialSqlService.Object,
                logger.Object);

            userCourseSqlService.ExistUserCourse(It.IsAny<int>());

            userCourseRepository.Verify(x => x.Exist(It.IsAny<Expression<Func<UserCourse, bool>>>()), Times.Once);
        }

        #endregion

        #region GetAllPassedAndProgressCoursesForUser

        [TestMethod]
        public void GetAllPassedAndProgressCoursesForUser_CallGet()
        {
            userCourseRepository.Setup(
                db => db.Get<Course>(It.IsAny<Expression<Func<UserCourse, Course>>>(),
                It.IsAny<Expression<Func<UserCourse, bool>>>()
                )).Returns(new List<Course>());

            UserCourseSqlService userCourseSqlService = new UserCourseSqlService(
                userCourseRepository.Object,
                userCourseMaterialSqlService.Object,
                logger.Object);

            userCourseSqlService.GetAllPassedAndProgressCoursesForUser(It.IsAny<int>());

            userCourseRepository.Verify(x => x.Get<Course>(It.IsAny<Expression<Func<UserCourse, Course>>>(),
                It.IsAny<Expression<Func<UserCourse, bool>>>()), Times.Once);
        }

        #endregion

        #region GetUserCourse

        [TestMethod]
        public void GetUserCourse_()
        {
            userCourseRepository.Setup(db => db.Get(It.IsAny<Expression<Func<UserCourse, bool>>>())).Returns(new List<UserCourse>());

            UserCourseSqlService userCourseSqlService = new UserCourseSqlService(
                userCourseRepository.Object,
                userCourseMaterialSqlService.Object,
                logger.Object);

            userCourseSqlService.GetUserCourse(It.IsAny<int>(), It.IsAny<int>());

            userCourseRepository.Verify(x => x.Get(It.IsAny<Expression<Func<UserCourse, bool>>>()), Times.Once); 
        }

        #endregion

        #region SetPassForUserCourse

        [TestMethod]
        public void SetPassForUserCourse_UserCourseNotExist_False()
        {
            List<UserCourse> userCourses = new List<UserCourse>();
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            userCourseRepository.Setup(db => db.Get(It.IsAny<Expression<Func<UserCourse, bool>>>())).Returns(userCourses);

            UserCourseSqlService userCourseSqlService = new UserCourseSqlService(
                userCourseRepository.Object,
                userCourseMaterialSqlService.Object,
                logger.Object);

            Assert.IsFalse(userCourseSqlService.SetPassForUserCourse(It.IsAny<int>(), It.IsAny<int>()));
        }

        [TestMethod]
        public void SetPassForUserCourse_UserCourseExist_True()
        {
            List<UserCourse> userCourses = new List<UserCourse>()
            {
                new UserCourse() { Id = 0, UserId = 0 }
            };

            userCourseRepository.Setup(db => db.Get(It.IsAny<Expression<Func<UserCourse, bool>>>())).Returns(userCourses);
            userCourseRepository.Setup(db => db.Update(It.IsAny<UserCourse>()));
            userCourseRepository.Setup(db => db.Save());

            UserCourseSqlService userCourseSqlService = new UserCourseSqlService(
                userCourseRepository.Object,
                userCourseMaterialSqlService.Object,
                logger.Object);

            userCourseSqlService.SetPassForUserCourse(It.IsAny<int>(), It.IsAny<int>());

            userCourseRepository.Verify(x => x.Update(It.IsAny<UserCourse>()));
            userCourseRepository.Verify(x => x.Save());
            Assert.IsTrue(userCourseSqlService.SetPassForUserCourse(It.IsAny<int>(), It.IsAny<int>()));
        }

        #endregion

        #region GetAllPassedCourse

        [TestMethod]
        public void GetAllPassedCourse_CallGetMethod()
        {
            userCourseRepository.Setup(
                db => db.Get<Course>(It.IsAny<Expression<Func<UserCourse, Course>>>(),
                It.IsAny<Expression<Func<UserCourse, bool>>>()
                )).Returns(new List<Course>());

            UserCourseSqlService userCourseSqlService = new UserCourseSqlService(
                userCourseRepository.Object,
                userCourseMaterialSqlService.Object,
                logger.Object);

            userCourseSqlService.GetAllPassedCourse(It.IsAny<int>());

            userCourseRepository.Verify(x => x.Get<Course>(It.IsAny<Expression<Func<UserCourse, Course>>>(),
                It.IsAny<Expression<Func<UserCourse, bool>>>()), Times.Once);
        }

        #endregion
    }
}
