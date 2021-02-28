using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.ServicesSql;
using EducationPortal.Domain.Entities;
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
    public class UserSqlServiceTests
    {
        private Mock<IRepository<User>> userRepository;
        private Mock<ICourseService> courseService;
        private Mock<IAuthorizedUser> authorizedUser;
        private Mock<IUserCourseSqlService> userCourseService;
        private Mock<IUserCourseMaterialSqlService> userCourseMaterialSqlService;
        private Mock<IUserMaterialSqlService> userMaterialSqlService;
        private Mock<IUserSkillSqlService> userSkillSqlService;
        private Mock<ICourseSkillService> courseSkillService;
        private Mock<IBLLLogger> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.userRepository = new Mock<IRepository<User>>();
            this.courseService = new Mock<ICourseService>();
            this.authorizedUser = new Mock<IAuthorizedUser>();
            this.userCourseService = new Mock<IUserCourseSqlService>();
            this.userCourseMaterialSqlService = new Mock<IUserCourseMaterialSqlService>();
            this.userMaterialSqlService = new Mock<IUserMaterialSqlService>();
            this.userSkillSqlService = new Mock<IUserSkillSqlService>();
            this.courseSkillService = new Mock<ICourseSkillService>();
            this.logger = new Mock<IBLLLogger>();
        }

        #region AddCourseInProgress

        [TestMethod]
        public void AddCourseInProgress_CourseNotExist_False()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            authorizedUser.Setup(db => db.User);
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).Returns(false);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsFalse(userSqlService.AddCourseInProgress(It.IsAny<int>()));
        }

        [TestMethod]
        public void AddCourseInProgress_AuthorizedUserNull_False()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).Returns(true);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                null,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsFalse(userSqlService.AddCourseInProgress(It.IsAny<int>()));
        }

        [TestMethod]
        public void AddCourseInProgress_AuthorizedUserNullCourseNotExist_False()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).Returns(false);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                null,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsFalse(userSqlService.AddCourseInProgress(It.IsAny<int>()));
        }

        [TestMethod]
        public void AddCourseInProgress_AuthorizedUserNotNullCourseExist_True()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userCourseService.Setup(db => db.AddCourseToUser(It.IsAny<int>(), It.IsAny<int>()));
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).Returns(true);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.AddCourseInProgress(It.IsAny<int>());

            userCourseService.Verify(x => x.AddCourseToUser(It.IsAny<int>(), It.IsAny<int>()));
            Assert.IsTrue(userSqlService.AddCourseInProgress(It.IsAny<int>()));
        }

        #endregion

        #region AddCourseToPassed

        [TestMethod]
        public void AddCourseToPassed_CallSetPassToCourseAndReturnTrue()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userCourseService.Setup(db => db.SetPassForUserCourse(It.IsAny<int>(), It.IsAny<int>()));

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.AddCourseToPassed(It.IsAny<int>());

            userCourseService.Verify(x => x.SetPassForUserCourse(It.IsAny<int>(), It.IsAny<int>()));
            Assert.IsTrue(userSqlService.AddCourseToPassed(It.IsAny<int>()));
        }

        #endregion

        #region AddSkill

        [TestMethod]
        public void AddSkill_SkillNull_False()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Skill skill = null;

            Assert.IsFalse(userSqlService.AddSkill(skill));
        }

        [TestMethod]
        public void AddSkill_SkillNotNull_True()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userSkillSqlService.Setup(db => db.AddSkillToUser(It.IsAny<int>(), It.IsAny<int>()));

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Skill skill = new Skill();
            userSqlService.AddSkill(skill);

            userSkillSqlService.Verify(db => db.AddSkillToUser(It.IsAny<int>(), It.IsAny<int>()));
            Assert.IsTrue(userSqlService.AddSkill(skill));
        }

        #endregion

        #region CreateUser

        [TestMethod]
        public void CreateUser_UserExist_False()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            User user = new User();

            Assert.IsFalse(userSqlService.CreateUser(It.IsAny<User>()));
        }

        [TestMethod]
        public void CreateUser_UserNull_False()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).Returns(false);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            User user = null;

            Assert.IsFalse(userSqlService.CreateUser(It.IsAny<User>()));
        }

        [TestMethod]
        public void CreateUser_UserNotNullUserExist_True()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).Returns(false);
            userRepository.Setup(db => db.Add(It.IsAny<User>()));
            userRepository.Setup(db => db.Save());

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            User user = new User();
            userSqlService.CreateUser(user);

            userRepository.Verify(x => x.Add(It.IsAny<User>()));
            userRepository.Verify(x => x.Save());
            Assert.IsTrue(userSqlService.CreateUser(user));
        }

        #endregion

        #region Delete

        [TestMethod]
        public void Delete_UserNotExist_False()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).Returns(false);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsFalse(userSqlService.Delete(It.IsAny<int>()));
        }

        [TestMethod]
        public void Delete_UserExist_True()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);
            userRepository.Setup(db => db.Delete(It.IsAny<int>()));
            userRepository.Setup(db => db.Save());

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.Delete(It.IsAny<int>());

            userRepository.Verify(x => x.Delete(It.IsAny<int>()));
            userRepository.Verify(x => x.Save());
            Assert.IsTrue(userSqlService.Delete(It.IsAny<int>()));
        }

        #endregion

        #region GetAllUserSkills

        [TestMethod]
        public void GetAllUserSkills_AuthorizedUserNull_Null()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                null,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsNull(userSqlService.GetAllUserSkills());
        }

        [TestMethod]
        public void GetAllUserSkills_AuthorizedUserNotNull_CallGetAllUserSkills()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User());
            userSkillSqlService.Setup(db => db.GetAllSkillInUser(It.IsAny<int>()));

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.GetAllUserSkills();

            userSkillSqlService.Verify(x => x.GetAllSkillInUser(It.IsAny<int>()), Times.Once);
        }

        #endregion

        #region GetAvailableCoursesForUser

        [TestMethod]
        public void GetAvailableCoursesForUser_AuthorizedUserNotNull_CallGetAndAvailable()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User());
            userCourseService.Setup(db => db.GetAllPassedAndProgressCoursesForUser(It.IsAny<int>())).Returns(new List<Course>());
            courseService.Setup(db => db.AvailableCourses(It.IsAny<List<Course>>())).Returns(new List<Course>());

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.GetAvailableCoursesForUser();

            userCourseService.Verify(x => x.GetAllPassedAndProgressCoursesForUser(It.IsAny<int>()), Times.Once);
            courseService.Verify(x => x.AvailableCourses(It.IsAny<List<Course>>()), Times.Once);
        }

        [TestMethod]
        public void GetAvailableCoursesForUser_AuthorizedUserNotNull_Null()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                null,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsNull(userSqlService.GetAvailableCoursesForUser());
        }

        #endregion

        #region GetListWithCoursesInProgress

        [TestMethod]
        public void GetListWithCoursesInProgress_AuthorizedUserNull_Null()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                null,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsNull(userSqlService.GetListWithCoursesInProgress());
        }

        [TestMethod]
        public void GetListWithCoursesInProgress_AuthorizedUserNotNull_Call()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User());
            userCourseService.Setup(db => db.GetAllCourseInProgress(It.IsAny<int>())).Returns(new List<Course>());

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.GetListWithCoursesInProgress();

            userCourseService.Verify(x => x.GetAllCourseInProgress(It.IsAny<int>()));
        }

        #endregion

        #region GetMaterialsFromCourseInProgress

        [TestMethod]
        public void GetMaterialsFromCourseInProgress_CourseNotExist_Null()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).Returns(false);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsNull(userSqlService.GetMaterialsFromCourseInProgress(It.IsAny<int>()));
        }

        [TestMethod]
        public void GetMaterialsFromCourseInProgress_CourseExist_CallGetUserCourse()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).Returns(true);
            userCourseMaterialSqlService.Setup(db => db.GetNotPassedMaterialsFromCourseInProgress(It.IsAny<int>()));
            userCourseService.Setup(db => db.GetUserCourse(It.IsAny<int>(), It.IsAny<int>())).Returns(new UserCourse() { Id = 0 });

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.GetMaterialsFromCourseInProgress(It.IsAny<int>());

            userCourseService.Verify(x => x.GetUserCourse(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            userCourseMaterialSqlService.Verify(x => x.GetNotPassedMaterialsFromCourseInProgress(It.IsAny<int>()));
        }

        #endregion

        #region GetSkillsFromCourseInProgress

        [TestMethod]
        public void GetSkillsFromCourseInProgress_CourseNotExist_Null()
        {
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).Returns(false);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsNull(userSqlService.GetSkillsFromCourseInProgress(It.IsAny<int>()));
        }

        [TestMethod]
        public void GetSkillsFromCourseInProgress_CourseExist_CallGetAllSkillsFromCourse()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).Returns(true);
            courseSkillService.Setup(db => db.GetAllSkillsFromCourse(It.IsAny<int>()));

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.GetSkillsFromCourseInProgress(It.IsAny<int>());

            courseSkillService.Verify(x => x.GetAllSkillsFromCourse(It.IsAny<int>()), Times.Once);
        }

        #endregion

        #region UpdateUser

        [TestMethod]
        public void UpdateUser_UserNotExist_False()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).Returns(false);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsFalse(userSqlService.UpdateUser(It.IsAny<User>()));
        }

        [TestMethod]
        public void UpdateUser_UserExist_CallUpdateAndSave()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);
            userRepository.Setup(db => db.Update(It.IsAny<User>()));
            userRepository.Setup(db => db.Save());

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.UpdateUser(It.IsAny<User>());

            userRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(userSqlService.UpdateUser(It.IsAny<User>()));
        }

        #endregion

        #region UpdateValueOfPassMaterialInProgress

        [TestMethod]
        public void UpdateValueOfPassMaterialInProgress_UserCourseMaterialNotExist_False()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userCourseService.Setup(db => db.GetUserCourse(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new UserCourse() { UserId = 0, CourseId = 0 });
            userCourseMaterialSqlService.Setup(db => db.SetPassToMaterial(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsFalse(userSqlService.UpdateValueOfPassMaterialInProgress(It.IsAny<int>(), It.IsAny<int>()));
        }

        [TestMethod]
        public void UpdateValueOfPassMaterialInProgress_UserCourseMaterialExist_True()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userCourseService.Setup(db => db.GetUserCourse(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new UserCourse() { UserId = 0, CourseId = 0 });
            userCourseMaterialSqlService.Setup(db => db.SetPassToMaterial(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            userMaterialSqlService.Setup(db => db.AddMaterialToUser(It.IsAny<int>(), It.IsAny<int>()));

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.UpdateValueOfPassMaterialInProgress(It.IsAny<int>(), It.IsAny<int>());

            userMaterialSqlService.Verify(x => x.AddMaterialToUser(It.IsAny<int>(), It.IsAny<int>()));
            Assert.IsTrue(userSqlService.UpdateValueOfPassMaterialInProgress(It.IsAny<int>(), It.IsAny<int>()));
        }

        #endregion

        #region ExistEmail

        [TestMethod]
        public void ExistEmail_CallExist()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>()));

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.ExistEmail(It.IsAny<Expression<Func<User, bool>>>());

            userRepository.Verify(x => x.Exist(It.IsAny<Expression<Func<User, bool>>>()));
        }

        #endregion

        #region GetAllPassedCourseFromUser

        [TestMethod]
        public void GetAllPassedCourseFromUser_AuthorizedUserNull_Null()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                null,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            Assert.IsNull(userSqlService.GetAllPassedCourseFromUser());
        }

        [TestMethod]
        public void GetAllPassedCourseFromUser_AuthorizedUserNotNull_CallGetAllPassedCourse()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userCourseService.Setup(db => db.GetAllPassedCourse(It.IsAny<int>()));

            UserSqlService userSqlService = new UserSqlService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                logger.Object);

            userSqlService.GetAllPassedCourseFromUser();

            userCourseService.Verify(x => x.GetAllPassedCourse(It.IsAny<int>()));
        }

        #endregion
    }
}
