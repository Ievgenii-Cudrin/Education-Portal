using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.ServicesSql;
using EducationPortal.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EducationPortal.BLL.Tests.ServicesSql
{
    [TestClass]
    public class CourseSkillSqlServiceTests
    {
        [TestMethod]
        public void AddMaterialToCourse_SkillNotExist_False()
        {
            Mock<IRepository<CourseSkill>> courseSkillRepo = new Mock<IRepository<CourseSkill>>();
            Mock<IRepository<Course>> courseRepo = new Mock<IRepository<Course>>();
            Mock<IRepository<Skill>> skillRepo = new Mock<IRepository<Skill>>();

            courseSkillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseSkill, bool>>>())).Returns(false);
            courseRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            skillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(false);

            CourseSkillSqlService courseSkillService = new CourseSkillSqlService(courseSkillRepo.Object, skillRepo.Object, courseRepo.Object);
            
            Assert.IsFalse(courseSkillService.AddSkillToCourse(0, 0));
        }

        [TestMethod]
        public void AddMaterialToCourse_CourseNotExist_False()
        {
            Mock<IRepository<CourseSkill>> courseSkillRepo = new Mock<IRepository<CourseSkill>>();
            Mock<IRepository<Course>> courseRepo = new Mock<IRepository<Course>>();
            Mock<IRepository<Skill>> skillRepo = new Mock<IRepository<Skill>>();

            courseSkillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseSkill, bool>>>())).Returns(false);
            courseRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);
            skillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(true);

            CourseSkillSqlService courseSkillService = new CourseSkillSqlService(courseSkillRepo.Object, skillRepo.Object, courseRepo.Object);
            
            Assert.IsFalse(courseSkillService.AddSkillToCourse(0, 0));
        }

        [TestMethod]
        public void AddMaterialToCourse_CourseSkillExist_False()
        {
            Mock<IRepository<CourseSkill>> courseSkillRepo = new Mock<IRepository<CourseSkill>>();
            Mock<IRepository<Course>> courseRepo = new Mock<IRepository<Course>>();
            Mock<IRepository<Skill>> skillRepo = new Mock<IRepository<Skill>>();

            courseSkillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseSkill, bool>>>())).Returns(true);
            courseRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            skillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(true);

            CourseSkillSqlService courseSkillService = new CourseSkillSqlService(courseSkillRepo.Object, skillRepo.Object, courseRepo.Object);
            
            Assert.IsFalse(courseSkillService.AddSkillToCourse(0, 0));
        }

        [TestMethod]
        public void AddMaterialToCourse_CourseSkillNotExistCourseExistSkillExist_True()
        {
            Mock<IRepository<CourseSkill>> courseSkillRepo = new Mock<IRepository<CourseSkill>>();
            Mock<IRepository<Course>> courseRepo = new Mock<IRepository<Course>>();
            Mock<IRepository<Skill>> skillRepo = new Mock<IRepository<Skill>>();

            courseSkillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<CourseSkill, bool>>>())).Returns(false);
            courseRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            skillRepo.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(true);
            courseSkillRepo.Setup(db => db.Save());

            CourseSkillSqlService courseSkillService = new CourseSkillSqlService(courseSkillRepo.Object, skillRepo.Object, courseRepo.Object);
            CourseSkill courseSkill = new CourseSkill()
            {
                CourseId = 0,
                SkillId = 0,
            };
            courseSkillService.AddSkillToCourse(0, 0);

            courseSkillRepo.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(courseSkillService.AddSkillToCourse(0, 0));
        }

        [TestMethod]
        public void GetAllMaterialsFromCourse_ReturnListMaterials()
        {
            Mock<IRepository<CourseSkill>> courseSkillRepo = new Mock<IRepository<CourseSkill>>();
            Mock<IRepository<Course>> courseRepo = new Mock<IRepository<Course>>();
            Mock<IRepository<Skill>> skillRepo = new Mock<IRepository<Skill>>();

            courseSkillRepo.Setup(db => db.Get<Skill>(It.IsAny<Expression<Func<CourseSkill, Skill>>>(),
                It.IsAny<Expression<Func<CourseSkill, bool>>>())).Returns(new List<Skill>());

            CourseSkillSqlService courseSkillSqlService = new CourseSkillSqlService(courseSkillRepo.Object, skillRepo.Object, courseRepo.Object);
            courseSkillSqlService.GetAllSkillsFromCourse(0);

            courseSkillRepo.Verify(x => x.Get<Skill>(x => x.Skill, x => x.CourseId == 0), Times.Once);
        }
    }
}
