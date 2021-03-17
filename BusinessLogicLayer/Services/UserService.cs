using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using Entities;

namespace EducationPortal.BLL.ServicesSql
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly ICourseService courseService;
        private readonly IAuthorizedUser authorizedUser;
        private readonly IUserCourseSqlService userCourseService;
        private readonly IUserCourseMaterialSqlService userCourseMaterialSqlService;
        private readonly IUserMaterialSqlService userMaterialSqlService;
        private readonly IUserSkillSqlService userSkillSqlService;
        private readonly ICourseSkillService courseSkillService;
        private readonly ICourseMaterialService courseMaterialService;

        public UserService(
            IRepository<User> uRepo,
            ICourseService courseService,
            IAuthorizedUser authUser,
            IUserCourseSqlService userCourseServ,
            IUserCourseMaterialSqlService userCourseMaterialSqlService,
            IUserMaterialSqlService userMaterialSqlService,
            IUserSkillSqlService userSkillSqlService,
            ICourseSkillService courseSkillService,
            ICourseMaterialService courseMaterialService)
        {
            this.userRepository = uRepo;
            this.courseService = courseService;
            this.authorizedUser = authUser;
            this.userCourseService = userCourseServ;
            this.userCourseMaterialSqlService = userCourseMaterialSqlService;
            this.userMaterialSqlService = userMaterialSqlService;
            this.userSkillSqlService = userSkillSqlService;
            this.courseSkillService = courseSkillService;
            this.courseMaterialService = courseMaterialService;
        }

        public async Task<bool> AddCourseInProgress(int courseId)
        {
            if (this.authorizedUser != null &&
                await this.courseService.ExistCourse(courseId))
            {
                await this.userCourseService.AddCourseToUser(this.authorizedUser.User.Id, courseId);
                return true;
            }

            return false;
        }

        public async Task<bool> AddCourseToPassed(int courseId)
        {
            await this.userCourseService.SetPassForUserCourse(this.authorizedUser.User.Id, courseId);
            return true;
        }

        public async Task<bool> AddSkill(Skill skill)
        {
            if (skill != null)
            {
                await this.userSkillSqlService.AddSkillToUser(this.authorizedUser.User.Id, skill.Id);
                return true;
            }

            return false;
        }

        public async Task AddSkills(List<Skill> skills)
        {
            foreach (var skill in skills)
            {
                await this.userSkillSqlService.AddSkillToUser(this.authorizedUser.User.Id, skill.Id);
            }
        }

        public async Task<bool> CreateUser(User user)
        {
            bool userExist = await this.userRepository.Exist(x => x.Email.ToLower().Equals(user.Email.ToLower()));

            if (user != null && !userExist)
            {
                await this.userRepository.Add(user);
                await this.userRepository.Save();
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (await this.userRepository.Exist(x => x.Id == id))
            {
                await this.userRepository.Delete(id);
                await this.userRepository.Save();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Skill>> GetAllUserSkills()
        {
            if (this.authorizedUser != null)
            {
                return await this.userSkillSqlService.GetAllSkillInUser(this.authorizedUser.User.Id);
            }

            return null;
        }

        public List<Course> GetAvailableCoursesForUser()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetListWithCoursesInProgress()
        {
            if (this.authorizedUser != null)
            {
                return await this.userCourseService.GetAllCourseInProgress(this.authorizedUser.User.Id);
            }

            return null;
        }

        public async Task<IEnumerable<Material>> GetMaterialsFromCourseInProgress(int courseId)
        {
            bool existCourseService = await this.courseService.ExistCourse(courseId);

            if (!existCourseService)
            {
                return null;
            }

            var userCourse = await this.userCourseService.GetUserCourse(this.authorizedUser.User.Id, courseId);
            int userCourseId = userCourse.Id;

            return await this.userCourseMaterialSqlService.GetNotPassedMaterialsFromCourseInProgress(userCourseId);
        }

        public async Task<IEnumerable<Skill>> GetSkillsFromCourseInProgress(int courseId)
        {
            if (await this.courseService.ExistCourse(courseId))
            {
                return await this.courseSkillService.GetAllSkillsFromCourse(courseId);
            }

            return null;
        }

        public void UpdateCourseInProgress(int courseInProgressNotFinishId, List<Material> updatedMaterials)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUser(User user)
        {
            if (await this.userRepository.Exist(x => x.Id == user.Id))
            {
                await this.userRepository.Update(user);
                await this.userRepository.Save();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateValueOfPassMaterialInProgress(int courseId, int materialId)
        {
            var userCourse = await this.userCourseService.GetUserCourse(this.authorizedUser.User.Id, courseId);
            bool success = await this.userCourseMaterialSqlService.SetPassToMaterial(userCourse.Id, materialId);

            if (success)
            {
                // Add pass material to user
                await this.userMaterialSqlService.AddMaterialToUser(this.authorizedUser.User.Id, materialId);
                return true;
            }

            return false;
        }

        public async Task<bool> ExistEmail(Expression<Func<User, bool>> predicat)
        {
            return await this.userRepository.Exist(predicat);
        }

        public async Task<IEnumerable<Course>> GetAllPassedCourseFromUser()
        {
            if (this.authorizedUser != null)
            {
                return await this.userCourseService.GetAllPassedCourse(this.authorizedUser.User.Id);
            }

            return null;
        }

        public async Task<IEnumerable<Material>> GetAllNotPassedMaterialsInCourse(int courseId)
        {
            var allMaterialsFromCourse = await this.courseMaterialService.GetAllMaterialsFromCourse(courseId);

            foreach (var material in allMaterialsFromCourse.ToList())
            {
                bool existThisMaterialInUser = await this.userMaterialSqlService.ExistMaterialInUser(this.authorizedUser.User.Id, material.Id);

                if (existThisMaterialInUser)
                {
                    material.IsPassed = true;
                }
            }

            return allMaterialsFromCourse;
        }
    }
}
