using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.ServicesSql;
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
    public class SkillSqlServiceTests
    {
        private Mock<IRepository<Skill>> skillRepository;
        private Mock<IBLLLogger> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.skillRepository = new Mock<IRepository<Skill>>();
            logger = new Mock<IBLLLogger>();
        }

        #region CreateSkill

        [TestMethod]
        public void CreateSkill_SkillUnique_CreateSkill()
        {
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(false);
            skillRepository.Setup(db => db.Add(It.IsAny<Skill>()));
            skillRepository.Setup(db => db.Save());

            SkillService skillSqlService = new SkillService(
                skillRepository.Object,
                logger.Object);

            Skill skill = new Skill();
            skillSqlService.CreateSkill(skill);

            skillRepository.Verify(x => x.Add(skill), Times.Once);
            skillRepository.Verify(x => x.Save(), Times.Once);
        }

        [TestMethod]
        public void CreateSkill_SkillNotUnique_Null()
        {
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(true);
            skillRepository.Setup(db => db.Get(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(new List<Skill>());

            SkillService skillSqlService = new SkillService(
                skillRepository.Object,
                logger.Object);

            Skill skill = new Skill();
            skillSqlService.CreateSkill(skill);

            skillRepository.Verify(x => x.Get(x => x.Name == skill.Name), Times.Once);
        }

        #endregion

        #region Delete

        [TestMethod]
        public void Delete_SkillExist_CallDeleteAndSave()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(true);
            skillRepository.Setup(db => db.Delete(It.IsAny<int>()));
            skillRepository.Setup(db => db.Save());

            SkillService skillSqlService = new SkillService(
                skillRepository.Object,
                logger.Object);
            skillSqlService.Delete(0);

            skillRepository.Verify(x => x.Delete(0), Times.Once);
            skillRepository.Verify(x => x.Save(), Times.Once);
        }

        [TestMethod]
        public void Delete_SkillNotExist_Nothing()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(false);
            skillRepository.Setup(db => db.Delete(It.IsAny<int>()));
            skillRepository.Setup(db => db.Save());

            SkillService skillSqlService = new SkillService(
                skillRepository.Object,
                logger.Object);
            skillSqlService.Delete(0);

            skillRepository.Verify(x => x.Delete(0), Times.Never);
            skillRepository.Verify(x => x.Save(), Times.Never);
        }

        #endregion

        #region GetSkill

        [TestMethod]
        public void GetSkill_SkillExist_CallGet()
        {
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(true);
            skillRepository.Setup(db => db.Get(It.IsAny<int>())).Returns(new Skill());

            SkillService skillSqlService = new SkillService(
                skillRepository.Object,
                logger.Object);
            skillSqlService.GetSkill(0);

            skillRepository.Verify(x => x.Get(0), Times.Once);
        }

        [TestMethod]
        public void GetSkill_SkillNotExist_Nothing()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(false);

            SkillService skillSqlService = new SkillService(
                skillRepository.Object,
                logger.Object);
            skillSqlService.GetSkill(0);

            skillRepository.Verify(x => x.Get(0), Times.Never);
        }

        #endregion

        #region UpdateSkill

        [TestMethod]
        public void UpdateSkill_SkillNull_Nothing()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            SkillService skillSqlService = new SkillService(
                skillRepository.Object,
                logger.Object);

            Skill skill = null;
            skillSqlService.UpdateSkill(null);

            skillRepository.Verify(x => x.Update(skill), Times.Never);
        }

        [TestMethod]
        public void UpdateSkill_SkillNotNull_CallUpdate()
        {
            logger.SetupGet(db => db.Logger).Returns(LogManager.GetCurrentClassLogger());
            skillRepository.Setup(db => db.Update(It.IsAny<Skill>()));
            SkillService skillSqlService = new SkillService(
                skillRepository.Object,
                logger.Object);

            Skill skill = new Skill();
            skillSqlService.UpdateSkill(skill);

            skillRepository.Verify(x => x.Update(skill), Times.Once);
        }

        #endregion

        #region ExistSkill

        [TestMethod]
        public void ExistSkill_CallExistMethod()
        {
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(true);

            SkillService skillSqlService = new SkillService(
                skillRepository.Object,
                logger.Object);

            skillSqlService.ExistSkill(0);

            skillRepository.Verify(x => x.Exist(x => x.Id == 0), Times.Once);
        }

        #endregion
    }
}
