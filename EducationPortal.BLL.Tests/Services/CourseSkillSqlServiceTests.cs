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
    public class CourseSkillSqlServiceTests
    {
        Mock<IRepository<CourseSkill>> courseSkillRepo;
        Mock<IRepository<Course>> courseRepo;
        Mock<IRepository<Skill>> skillRepo;
        Mock<ILogger> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.courseSkillRepo = new Mock<IRepository<CourseSkill>>();
            this.courseRepo = new Mock<IRepository<Course>>();
            this.skillRepo = new Mock<IRepository<Skill>>();
            this.logger = new Mock<ILogger>();
        }

        [TestMethod]
        public async Task AddMaterialToCourse_SkillNotExist_False()
        {
            courseSkillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseSkill, bool>>>())).ReturnsAsync(false);
            courseRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);
            skillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).ReturnsAsync(false);

            CourseSkillService courseSkillService = new CourseSkillService(
                courseSkillRepo.Object,
                skillRepo.Object,
                courseRepo.Object);
            
            Assert.IsFalse(await courseSkillService.AddSkillToCourse(0, 0));
        }

        [TestMethod]
        public async Task AddMaterialToCourse_CourseNotExist_False()
        {
            courseSkillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseSkill, bool>>>())).ReturnsAsync(false);
            courseRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(false);
            skillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).ReturnsAsync(true);

            CourseSkillService courseSkillService = new CourseSkillService(
                courseSkillRepo.Object,
                skillRepo.Object,
                courseRepo.Object);

            Assert.IsFalse(await courseSkillService.AddSkillToCourse(0, 0));
        }

        [TestMethod]
        public async Task AddMaterialToCourse_CourseSkillExist_False()
        {
            courseSkillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseSkill, bool>>>())).ReturnsAsync(true);
            courseRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);
            skillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).ReturnsAsync(true);

            CourseSkillService courseSkillService = new CourseSkillService(
                courseSkillRepo.Object,
                skillRepo.Object,
                courseRepo.Object);

            Assert.IsFalse(await courseSkillService.AddSkillToCourse(0, 0));
        }

        [TestMethod]
        public async Task AddMaterialToCourse_CourseSkillNotExistCourseExistSkillExist_True()
        {
            Mock<IRepository<CourseSkill>> courseSkillRepo = new Mock<IRepository<CourseSkill>>();
            Mock<IRepository<Course>> courseRepo = new Mock<IRepository<Course>>();
            Mock<IRepository<Skill>> skillRepo = new Mock<IRepository<Skill>>();

            courseSkillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseSkill, bool>>>())).ReturnsAsync(false);
            courseRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(true);
            skillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).ReturnsAsync(true);
            courseSkillRepo.Setup(db => db.Save());

            CourseSkillService courseSkillService = new CourseSkillService(
                courseSkillRepo.Object,
                skillRepo.Object,
                courseRepo.Object);

            CourseSkill courseSkill = new CourseSkill()
            {
                CourseId = 0,
                SkillId = 0,
            };
            courseSkillService.AddSkillToCourse(0, 0);

            courseSkillRepo.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(await courseSkillService.AddSkillToCourse(0, 0));
        }

        [TestMethod]
        public void GetAllMaterialsFromCourse_ReturnListMaterials()
        {
            Mock<IRepository<CourseSkill>> courseSkillRepo = new Mock<IRepository<CourseSkill>>();
            Mock<IRepository<Course>> courseRepo = new Mock<IRepository<Course>>();
            Mock<IRepository<Skill>> skillRepo = new Mock<IRepository<Skill>>();

            courseSkillRepo.Setup(db => db.Get<Skill>(It.IsAny<Expression<Func<CourseSkill, Skill>>>(),
                It.IsAny<Expression<Func<CourseSkill, bool>>>())).ReturnsAsync(new List<Skill>());

            CourseSkillService courseSkillService = new CourseSkillService(
                courseSkillRepo.Object,
                skillRepo.Object,
                courseRepo.Object);

            courseSkillService.GetAllSkillsFromCourse(0);

            courseSkillRepo.Verify(x => x.Get<Skill>(x => x.Skill, x => x.CourseId == 0), Times.Once);
        }
    }
}
