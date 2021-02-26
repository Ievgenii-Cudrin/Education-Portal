using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.ServicesSql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

        [TestInitialize]
        public void SetUp()
        {
            this.skillRepository = new Mock<IRepository<Skill>>();
        }

        #region CreateSkill

        [TestMethod]
        public void CreateSkill_SkillUnique_CreateSkill()
        {
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(false);
            skillRepository.Setup(db => db.Add(It.IsAny<Skill>()));
            skillRepository.Setup(db => db.Save());

            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);

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

            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);

            Skill skill = new Skill();
            skillSqlService.CreateSkill(skill);

            skillRepository.Verify(x => x.Get(x => x.Name == skill.Name), Times.Once);
        }

        #endregion

        #region Delete

        [TestMethod]
        public void Delete_SkillExist_CallDeleteAndSave()
        {
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(true);
            skillRepository.Setup(db => db.Delete(It.IsAny<int>()));
            skillRepository.Setup(db => db.Save());

            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);
            skillSqlService.Delete(0);

            skillRepository.Verify(x => x.Delete(0), Times.Once);
            skillRepository.Verify(x => x.Save(), Times.Once);
        }

        [TestMethod]
        public void Delete_SkillNotExist_Nothing()
        {
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(false);
            skillRepository.Setup(db => db.Delete(It.IsAny<int>()));
            skillRepository.Setup(db => db.Save());

            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);
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

            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);
            skillSqlService.GetSkill(0);

            skillRepository.Verify(x => x.Get(0), Times.Once);
        }

        [TestMethod]
        public void GetSkill_SkillNotExist_Nothing()
        {
            skillRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(false);

            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);
            skillSqlService.GetSkill(0);

            skillRepository.Verify(x => x.Get(0), Times.Never);
        }

        #endregion

        #region GetSkillByName

        [TestMethod]
        public void GetSkillByName_StringEmpty_Null()
        {
            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);

            string name = string.Empty;

            Assert.IsNull(skillSqlService.GetSkillByName(name));
        }

        [TestMethod]
        public void GetSkillByName_StringNullEmpty_Null()
        {
            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);

            string name = null;

            Assert.IsNull(skillSqlService.GetSkillByName(name));
        }

        [TestMethod]
        public void GetSkillByName_StringNotNullNotEmpty_Null()
        {
            skillRepository.Setup(db => db.Get(It.IsAny<Expression<Func<Skill, bool>>>())).Returns(new List<Skill>());

            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);

            string name = "string";
            skillSqlService.GetSkillByName(name);

            skillRepository.Verify(x => x.Get(x => x.Name == name), Times.Once);
        }

        #endregion

        #region UpdateSkill

        [TestMethod]
        public void UpdateSkill_SkillNull_Nothing()
        {
            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);

            Skill skill = null;
            skillSqlService.UpdateSkill(null);

            skillRepository.Verify(x => x.Update(skill), Times.Never);
        }

        [TestMethod]
        public void UpdateSkill_SkillNotNull_CallUpdate()
        {
            skillRepository.Setup(db => db.Update(It.IsAny<Skill>()));
            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);

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

            SkillSqlService skillSqlService = new SkillSqlService(skillRepository.Object);

            skillSqlService.ExistSkill(0);

            skillRepository.Verify(x => x.Exist(x => x.Id == 0), Times.Once);
        }

        #endregion
    }
}
