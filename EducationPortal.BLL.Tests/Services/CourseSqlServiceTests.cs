using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.ServicesSql;
using Entities;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BLL.Tests.ServicesSql
{
    [TestClass]
    public class CourseSqlServiceTests
    {
        private Mock<IRepository<Course>> courseRepository;
        private Mock<ICourseMaterialService> courseMaterailService;
        private Mock<ICourseSkillService> courseSkillService;
        private Mock<IAuthorizedUser> authorizedUser;
        private Mock<ILogger<CourseService>> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.courseRepository = new Mock<IRepository<Course>>();
            this.courseMaterailService = new Mock<ICourseMaterialService>();
            this.courseSkillService = new Mock<ICourseSkillService>();
            this.authorizedUser = new Mock<IAuthorizedUser>();
            this.logger = new Mock<ILogger<CourseService>>();
        }

        #region AddMaterialToCourseTests

        [TestMethod]
        public async Task AddMaterialToCourse_CourseNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Material material = new Material();

            Assert.IsFalse(await courseSqlService.AddMaterialToCourse(0, material));
        }

        [TestMethod]
        public async Task AddMaterialToCourse_SkillNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Material material = new Material();

            Assert.IsFalse(await courseSqlService.AddMaterialToCourse(0, material));
        }

        [TestMethod]
        public async Task AddMaterialToCourse_CourseAndSkillNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Material material = new Material();

            Assert.IsFalse(await courseSqlService.AddMaterialToCourse(0, material));
        }

        [TestMethod]
        public async Task AddMaterialToCourse_CourseAndSkillExist_True()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);
            courseMaterailService.Setup(db => db.AddMaterialToCourse(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Material material = new Material();

            Assert.IsTrue(await courseSqlService.AddMaterialToCourse(0, material));
        }

        #endregion

        #region AddSkillToCourseTests

        [TestMethod]
        public async Task AddSkillToCourse_CourseNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Skill skill = new Skill();

            Assert.IsFalse(await courseSqlService.AddSkillToCourse(0, skill));
        }

        [TestMethod]
        public async Task AddSkillToCourse_SkillNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Skill skill = new Skill();

            Assert.IsFalse(await courseSqlService.AddSkillToCourse(0, skill));
        }

        [TestMethod]
        public async Task AddSkillToCourse_CourseAndSkillNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Skill skill = new Skill();

            Assert.IsFalse(await courseSqlService.AddSkillToCourse(0, skill));
        }

        [TestMethod]
        public async Task AddSkillToCourse_CourseAndSkillExist_True()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);
            courseSkillService.Setup(db => db.AddSkillToCourse(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Skill skill = new Skill();

            Assert.IsTrue(await courseSqlService.AddSkillToCourse(0, skill));
        }

        #endregion

        #region CreateCourseTests

        [TestMethod]
        public async Task CreateCourse_CourseNull_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Course course = null;

            Assert.IsFalse(await courseSqlService.CreateCourse(course));
        }

        [TestMethod]
        public async Task CreateCourse_CourseExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Course course = new Course();

            Assert.IsFalse(await courseSqlService.CreateCourse(course));
        }

        [TestMethod]
        public async Task CreateCourse_CourseNotNullAndCourseNotExist_True()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);
            courseRepository.Setup(db => db.Save());

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Course course = new Course();
            courseSqlService.CreateCourse(course);

            courseRepository.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(await courseSqlService.CreateCourse(course));
        }

        #endregion

        #region Delete

        [TestMethod]
        public async Task Delete_CourseNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Assert.IsFalse(await courseSqlService.Delete(0));
        }

        [TestMethod]
        public async Task Delete_CourseExist_True()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);
            courseRepository.Setup(db => db.Delete(It.IsAny<int>()));
            courseRepository.Setup(db => db.Save());

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            courseSqlService.Delete(0);

            courseRepository.Verify(x => x.Delete(0), Times.Once);
            courseRepository.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(await courseSqlService.Delete(0));
        }

        #endregion

        #region GetMaterialsFromCourse

        [TestMethod]
        public void GetMaterialsFromCourse_CourseExist_CallMaterialAndCourseMaterialServices()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);
            courseMaterailService.Setup(db => db.GetAllMaterialsFromCourse(It.IsAny<int>())).ReturnsAsync(new List<Material>());

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            courseSqlService.GetMaterialsFromCourse(0);

            courseMaterailService.Verify(x => x.GetAllMaterialsFromCourse(0), Times.Once);
        }

        [TestMethod]
        public void GetMaterialsFromCourse_CourseNotExist_ReturnNull()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);
            courseMaterailService.Setup(db => db.GetAllMaterialsFromCourse(It.IsAny<int>())).ReturnsAsync(new List<Material>());

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Assert.IsNull(courseSqlService.GetMaterialsFromCourse(0));
        }

        #endregion

        #region GetSkillsFromCourse

        [TestMethod]
        public void GetSkillsFromCourse_CourseExist_CallCourseSkillService()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);
            courseSkillService.Setup(db => db.GetAllSkillsFromCourse(It.IsAny<int>())).ReturnsAsync(new List<Skill>());

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            courseSqlService.GetSkillsFromCourse(0);

            courseSkillService.Verify(x => x.GetAllSkillsFromCourse(0), Times.Once);
        }

        [TestMethod]
        public void GetSkillsFromCourse_CourseNotExist_Null()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);
            courseSkillService.Setup(db => db.GetAllSkillsFromCourse(It.IsAny<int>())).ReturnsAsync(new List<Skill>());

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Assert.IsNull(courseSqlService.GetSkillsFromCourse(0));
        }

        #endregion

        #region UpdateCourse

        [TestMethod]
        public async Task UpdateCourse_CourseNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);
            courseRepository.Setup(db => db.Update(It.IsAny<Course>()));
            courseRepository.Setup(db => db.Save());

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Course course = new Course();

            Assert.IsFalse(await courseSqlService.UpdateCourse(course));
        }

        [TestMethod]
        public async Task UpdateCourse_CourseExist_UpdateCourse()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);
            courseRepository.Setup(db => db.Update(It.IsAny<Course>()));
            courseRepository.Setup(db => db.Save());

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Course course = new Course()
            {
                Name = "1",
                Id = 0,
                Description = "desc"
            };

            courseSqlService.UpdateCourse(course);
            courseRepository.Verify(x => x.Update(course));
            courseRepository.Verify(x => x.Save());
            Assert.IsTrue(await courseSqlService.UpdateCourse(course));
        }

        #endregion

        #region ExistCourse

        [TestMethod]
        public async Task ExistCourse_CourseExist_True()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Assert.IsTrue(await courseSqlService.ExistCourse(0));
        }

        [TestMethod]
        public async Task ExistCourse_CourseNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);

            CourseService courseSqlService = new CourseService(
                courseRepository.Object,
                courseMaterailService.Object,
                courseSkillService.Object,
                logger.Object,
                authorizedUser.Object);

            Assert.IsFalse(await courseSqlService.ExistCourse(0));
        }

        #endregion
    }
}
