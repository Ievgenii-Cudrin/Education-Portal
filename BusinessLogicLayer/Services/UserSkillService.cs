using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Domain.Entities;

namespace EducationPortal.BLL.ServicesSql
{
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
                await this.userSkillRepository.Update(userSkill);
            }
            else
            {
                userSkill = new UserSkill()
                {
                    UserId = userId,
                    SkillId = skillId,
                    CountOfPoint = 1,
                };

                await this.userSkillRepository.Add(userSkill);
            }

            await this.userSkillRepository.Save();
        }

        public async Task<IEnumerable<Skill>> GetAllSkillInUser(int userId)
        {
            return await this.userSkillRepository.Get<Skill>(x => x.Skill, x => x.UserId == userId);
        }

        public async Task<int> GetCountOfUserSkill(int userId, int skillId)
        {
            var userSkill = await this.userSkillRepository.GetOne(x => x.UserId == userId && x.SkillId == skillId);
            return userSkill.CountOfPoint;
        }

        public async Task<IEnumerable<UserSkill>> GetAllUSerSkillsWithInclude(int userId)
        {
            return await this.userSkillRepository.GetWithInclude(x => x.UserId == userId, x => x.Skill);
        }
    }
}
