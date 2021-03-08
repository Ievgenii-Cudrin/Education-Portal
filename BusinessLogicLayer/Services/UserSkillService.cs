namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;
    using NLog;

    public class UserSkillService : IUserSkillSqlService
    {
        private readonly IRepository<UserSkill> userSkillRepository;

        public UserSkillService(IRepository<UserSkill> userSkillRepository)
        {
            this.userSkillRepository = userSkillRepository;
        }

        public async Task AddSkillToUser(int userId, int skillId)
        {
            var userSkill = await this.userSkillRepository.GetOne(x => x.UserId == userId && x.SkillId == skillId);

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

                await this.userSkillRepository.Add(userSkill);
            }
        }

        public async Task<List<Skill>> GetAllSkillInUser(int userId)
        {
            return await this.userSkillRepository.Get<Skill>(x => x.Skill, x => x.UserId == userId);
        }

        public async Task<int> GetCountOfUserSkill(int userId, int skillId)
        {
            var userSkill = await this.userSkillRepository.GetOne(x => x.UserId == userId && x.SkillId == skillId);
            return userSkill.CountOfPoint;
        }
    }
}
