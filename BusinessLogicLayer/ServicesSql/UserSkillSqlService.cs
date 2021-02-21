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

        public UserSkillSqlService(
            IEnumerable<IRepository<UserSkill>> userSkillRepository)
        {
            this.userSkillRepository = userSkillRepository.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<UserSkill>));
        }

        public void AddSkillToUser(int userId, int skillId)
        {
            UserSkill userSkill = this.userSkillRepository.Get(x => x.UserId == userId && x.SkillId == skillId).FirstOrDefault();

            if (userSkill != null)
            {
                userSkill.CountOfPoint++;
                this.userSkillRepository.Update(userSkill);
            }
            else
            {
                userSkill = new UserSkill()
                {
                    UserId = userId,
                    SkillId = skillId,
                };

                this.userSkillRepository.Add(userSkill);
            }

            this.userSkillRepository.Save();
        }

        public List<Skill> GetAllSkillInUser(int userId)
        {
            var s = this.userSkillRepository.Get<Skill>(x => x.Skill, x => x.UserId == userId).ToList();
            s.
            return this.userSkillRepository.Get<Skill>(x => x.Skill, x => x.UserId == userId).ToList();
        }
    }
}
