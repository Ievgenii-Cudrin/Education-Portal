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
using System.Threading.Tasks;

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
        private Mock<ICourseMaterialService> courseMaterialService;

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
            this.courseMaterialService = new Mock<ICourseMaterialService>();
        }

        #region AddCourseInProgress

        [TestMethod]
        public async Task AddCourseInProgress_CourseNotExist_False()
        {
            authorizedUser.Setup(db => db.User);
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).ReturnsAsync(false);

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            Assert.IsFalse(await userSqlService.AddCourseInProgress(It.IsAny<int>()));
        }

        [TestMethod]
        public async Task AddCourseInProgress_AuthorizedUserNull_False()
        {
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).ReturnsAsync(true);

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                null,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            Assert.IsFalse(await userSqlService.AddCourseInProgress(It.IsAny<int>()));
        }

        [TestMethod]
        public async Task AddCourseInProgress_AuthorizedUserNullCourseNotExist_False()
        {
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).ReturnsAsync(false);

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                null,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            Assert.IsFalse(await userSqlService.AddCourseInProgress(It.IsAny<int>()));
        }

        [TestMethod]
        public async Task AddCourseInProgress_AuthorizedUserNotNullCourseExist_True()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userCourseService.Setup(db => db.AddCourseToUser(It.IsAny<int>(), It.IsAny<int>()));
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).ReturnsAsync(true);

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.AddCourseInProgress(It.IsAny<int>());

            userCourseService.Verify(x => x.AddCourseToUser(It.IsAny<int>(), It.IsAny<int>()));
            Assert.IsTrue(await userSqlService.AddCourseInProgress(It.IsAny<int>()));
        }

        #endregion

        #region AddCourseToPassed

        [TestMethod]
        public async Task AddCourseToPassed_CallSetPassToCourseAndReturnTrue()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userCourseService.Setup(db => db.SetPassForUserCourse(It.IsAny<int>(), It.IsAny<int>()));

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.AddCourseToPassed(It.IsAny<int>());

            userCourseService.Verify(x => x.SetPassForUserCourse(It.IsAny<int>(), It.IsAny<int>()));
            Assert.IsTrue(await userSqlService.AddCourseToPassed(It.IsAny<int>()));
        }

        #endregion

        #region AddSkill

        [TestMethod]
        public async Task AddSkill_SkillNull_False()
        {
            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            Skill skill = null;

            Assert.IsFalse(await userSqlService.AddSkill(skill));
        }

        [TestMethod]
        public async Task AddSkill_SkillNotNull_True()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userSkillSqlService.Setup(db => db.AddSkillToUser(It.IsAny<int>(), It.IsAny<int>()));

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            Skill skill = new Skill();
            userSqlService.AddSkill(skill);

            userSkillSqlService.Verify(db => db.AddSkillToUser(It.IsAny<int>(), It.IsAny<int>()));
            Assert.IsTrue(await userSqlService.AddSkill(skill));
        }

        #endregion

        #region CreateUser

        [TestMethod]
        public async Task CreateUser_UserExist_False()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            User user = new User();

            Assert.IsFalse(await userSqlService.CreateUser(It.IsAny<User>()));
        }

        [TestMethod]
        public async Task CreateUser_UserNull_False()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(false);

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            User user = null;

            Assert.IsFalse(await userSqlService.CreateUser(It.IsAny<User>()));
        }

        [TestMethod]
        public async Task CreateUser_UserNotNullUserExist_True()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(false);
            userRepository.Setup(db => db.Add(It.IsAny<User>()));
            userRepository.Setup(db => db.Save());

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            User user = new User();
            userSqlService.CreateUser(user);

            userRepository.Verify(x => x.Add(It.IsAny<User>()));
            userRepository.Verify(x => x.Save());
            Assert.IsTrue(await userSqlService.CreateUser(user));
        }

        #endregion

        #region Delete

        [TestMethod]
        public async Task Delete_UserNotExist_False()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(false);

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            Assert.IsFalse(await userSqlService.Delete(It.IsAny<int>()));
        }

        [TestMethod]
        public async Task Delete_UserExist_True()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);
            userRepository.Setup(db => db.Delete(It.IsAny<int>()));
            userRepository.Setup(db => db.Save());

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.Delete(It.IsAny<int>());

            userRepository.Verify(x => x.Delete(It.IsAny<int>()));
            userRepository.Verify(x => x.Save());
            Assert.IsTrue(await userSqlService.Delete(It.IsAny<int>()));
        }

        #endregion

        #region GetAllUserSkills

        [TestMethod]
        public void GetAllUserSkills_AuthorizedUserNotNull_CallGetAllUserSkills()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User());
            userSkillSqlService.Setup(db => db.GetAllSkillInUser(It.IsAny<int>()));

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.GetAllUserSkills();

            userSkillSqlService.Verify(x => x.GetAllSkillInUser(It.IsAny<int>()), Times.Once);
        }

        #endregion

        #region GetListWithCoursesInProgress

        [TestMethod]
        public void GetListWithCoursesInProgress_AuthorizedUserNotNull_Call()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User());
            userCourseService.Setup(db => db.GetAllCourseInProgress(It.IsAny<int>())).ReturnsAsync(new List<Course>());

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.GetListWithCoursesInProgress();

            userCourseService.Verify(x => x.GetAllCourseInProgress(It.IsAny<int>()));
        }

        #endregion

        #region GetMaterialsFromCourseInProgress

        [TestMethod]
        public async Task GetMaterialsFromCourseInProgress_CourseExist_CallGetUserCourse()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).ReturnsAsync(true);
            userCourseMaterialSqlService.Setup(db => db.GetNotPassedMaterialsFromCourseInProgress(It.IsAny<int>()));
            userCourseService.Setup(db => db.GetUserCourse(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new UserCourse() { Id = 0 });

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.GetMaterialsFromCourseInProgress(It.IsAny<int>());

            userCourseService.Verify(x => x.GetUserCourse(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            userCourseMaterialSqlService.Verify(x => x.GetNotPassedMaterialsFromCourseInProgress(It.IsAny<int>()));
        }

        #endregion

        #region GetSkillsFromCourseInProgress

        [TestMethod]
        public void GetSkillsFromCourseInProgress_CourseExist_CallGetAllSkillsFromCourse()
        {
            courseService.Setup(db => db.ExistCourse(It.IsAny<int>())).ReturnsAsync(true);
            courseSkillService.Setup(db => db.GetAllSkillsFromCourse(It.IsAny<int>()));

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.GetSkillsFromCourseInProgress(It.IsAny<int>());

            courseSkillService.Verify(x => x.GetAllSkillsFromCourse(It.IsAny<int>()), Times.Once);
        }

        #endregion

        #region UpdateUser

        [TestMethod]
        public async Task UpdateUser_UserNotExist_False()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(false);

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            Assert.IsFalse(await userSqlService.UpdateUser(It.IsAny<User>()));
        }

        [TestMethod]
        public async Task UpdateUser_UserExist_CallUpdateAndSave()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);
            userRepository.Setup(db => db.Update(It.IsAny<User>()));
            userRepository.Setup(db => db.Save());

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.UpdateUser(It.IsAny<User>());

            userRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(await userSqlService.UpdateUser(It.IsAny<User>()));
        }

        #endregion

        #region UpdateValueOfPassMaterialInProgress

        [TestMethod]
        public async Task UpdateValueOfPassMaterialInProgress_UserCourseMaterialNotExist_False()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userCourseService.Setup(db => db.GetUserCourse(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new UserCourse() { UserId = 0, CourseId = 0 });
            userCourseMaterialSqlService.Setup(db => db.SetPassToMaterial(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            Assert.IsFalse(await userSqlService.UpdateValueOfPassMaterialInProgress(It.IsAny<int>(), It.IsAny<int>()));
        }

        [TestMethod]
        public async Task UpdateValueOfPassMaterialInProgress_UserCourseMaterialExist_True()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userCourseService.Setup(db => db.GetUserCourse(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new UserCourse() { UserId = 0, CourseId = 0 });
            userCourseMaterialSqlService.Setup(db => db.SetPassToMaterial(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
            userMaterialSqlService.Setup(db => db.AddMaterialToUser(It.IsAny<int>(), It.IsAny<int>()));

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.UpdateValueOfPassMaterialInProgress(It.IsAny<int>(), It.IsAny<int>());

            userMaterialSqlService.Verify(x => x.AddMaterialToUser(It.IsAny<int>(), It.IsAny<int>()));
            Assert.IsTrue(await userSqlService.UpdateValueOfPassMaterialInProgress(It.IsAny<int>(), It.IsAny<int>()));
        }

        #endregion

        #region ExistEmail

        [TestMethod]
        public void ExistEmail_CallExist()
        {
            userRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<User, bool>>>()));

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.ExistEmail(It.IsAny<Expression<Func<User, bool>>>());

            userRepository.Verify(x => x.Exist(It.IsAny<Expression<Func<User, bool>>>()));
        }

        #endregion

        #region GetAllPassedCourseFromUser

        [TestMethod]
        public void GetAllPassedCourseFromUser_AuthorizedUserNotNull_CallGetAllPassedCourse()
        {
            authorizedUser.SetupGet(db => db.User).Returns(new User() { Id = 0 });
            userCourseService.Setup(db => db.GetAllPassedCourse(It.IsAny<int>()));

            UserService userSqlService = new UserService(
                userRepository.Object,
                courseService.Object,
                authorizedUser.Object,
                userCourseService.Object,
                userCourseMaterialSqlService.Object,
                userMaterialSqlService.Object,
                userSkillSqlService.Object,
                courseSkillService.Object,
                courseMaterialService.Object);

            userSqlService.GetAllPassedCourseFromUser();

            userCourseService.Verify(x => x.GetAllPassedCourse(It.IsAny<int>()));
        }

        #endregion
    }
}
