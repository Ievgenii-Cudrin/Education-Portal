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
    public class UserSkillSqlServiceTests
    {
        private Mock<IRepository<UserSkill>> userSkillRepository;
        private Mock<ILogger> logger;

        [TestInitialize]
        public void SetUp()
        {
            this.userSkillRepository = new Mock<IRepository<UserSkill>>();
            this.logger = new Mock<ILogger>();
        }

        #region AddSkillToUser

        [TestMethod]
        public void AddSkillToUser_UserSkillNotExist_Add()
        {
            List<UserSkill> userSkills = new List<UserSkill>();

            userSkillRepository.Setup(db => db.Get(It.IsAny<Expression<Func<UserSkill, bool>>>())).ReturnsAsync(userSkills);
            userSkillRepository.Setup(db => db.Add(It.IsAny<UserSkill>()));
            userSkillRepository.Setup(db => db.Save());

            UserSkillService userSkillSqlService = new UserSkillService(
                userSkillRepository.Object);

            userSkillSqlService.AddSkillToUser(It.IsAny<int>(), It.IsAny<int>());

            userSkillRepository.Verify(x => x.Add(It.IsAny<UserSkill>()));
            userSkillRepository.Verify(x => x.Save());
        }

        #endregion

        #region GetAllSkillInUser

        [TestMethod]
        public async Task GetAllSkillInUser_CallGet()
        {
            userSkillRepository.Setup(
                db => db.Get<Skill>(It.IsAny<Expression<Func<UserSkill, Skill>>>(),
                It.IsAny<Expression<Func<UserSkill, bool>>>()
                )).ReturnsAsync(new List<Skill>());

            UserSkillService userSkillSqlService = new UserSkillService(
                 userSkillRepository.Object);

            await userSkillSqlService.GetAllSkillInUser(It.IsAny<int>());

            userSkillRepository.Verify(x => x.Get<Skill>(It.IsAny<Expression<Func<UserSkill, Skill>>>(),
                It.IsAny<Expression<Func<UserSkill, bool>>>()
                ), Times.Once);
        }

        #endregion
    }
}
