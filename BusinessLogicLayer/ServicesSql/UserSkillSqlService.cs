namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;

    public class UserSkillSqlService : IUserSkillSqlService
    {
        private readonly IRepository<UserSkill> userSkillRepository;
        private static IBLLLogger logger;

        public UserSkillSqlService(IRepository<UserSkill> userSkillRepository,
            IBLLLogger log)
        {
            this.userSkillRepository = userSkillRepository;
            logger = log;
        }

        public void AddSkillToUser(int userId, int skillId)
        {
            UserSkill userSkill = this.userSkillRepository.Get(x => x.UserId == userId && x.SkillId == skillId).FirstOrDefault();

            if (userSkill != null)
            {
                userSkill.CountOfPoint++;
                this.userSkillRepository.Update(userSkill);
                logger.Logger.Debug("Add point to exist skill in user - " + DateTime.Now);
            }
            else
            {
                userSkill = new UserSkill()
                {
                    UserId = userId,
                    SkillId = skillId,
                };

                this.userSkillRepository.Add(userSkill);
                logger.Logger.Debug("Add new skill - " + DateTime.Now);
            }

            this.userSkillRepository.Save();
        }

        public List<Skill> GetAllSkillInUser(int userId)
        {
            return this.userSkillRepository.Get<Skill>(x => x.Skill, x => x.UserId == userId).ToList();
        }
    }
}
