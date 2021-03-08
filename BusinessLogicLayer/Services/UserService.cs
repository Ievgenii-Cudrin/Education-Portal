namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Migrations;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;
    using Entities;
    using NLog;

    public class UserService : IUserService
    {
        private IRepository<User> userRepository;
        private ICourseService courseService;
        private IAuthorizedUser authorizedUser;
        private IUserCourseSqlService userCourseService;
        private IUserCourseMaterialSqlService userCourseMaterialSqlService;
        private IUserMaterialSqlService userMaterialSqlService;
        private IUserSkillSqlService userSkillSqlService;
        private ICourseSkillService courseSkillService;

        public UserService(
            IRepository<User> uRepo,
            ICourseService courseService,
            IAuthorizedUser authUser,
            IUserCourseSqlService userCourseServ,
            IUserCourseMaterialSqlService userCourseMaterialSqlService,
            IUserMaterialSqlService userMaterialSqlService,
            IUserSkillSqlService userSkillSqlService,
            ICourseSkillService courseSkillService)
        {
            this.userRepository = uRepo;
            this.courseService = courseService;
            this.authorizedUser = authUser;
            this.userCourseService = userCourseServ;
            this.userCourseMaterialSqlService = userCourseMaterialSqlService;
            this.userMaterialSqlService = userMaterialSqlService;
            this.userSkillSqlService = userSkillSqlService;
            this.courseSkillService = courseSkillService;
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

        public async Task<bool> CreateUser(User user)
        {
            bool userExist = await this.userRepository.Exist(x => x.Email.ToLower().Equals(user.Email.ToLower()));

            if (user != null && !userExist)
            {
                await this.userRepository.Add(user);
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
                return true;
            }

            return false;
        }

        public async Task<IList<Skill>> GetAllUserSkills()
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

        public async Task<IList<Course>> GetListWithCoursesInProgress()
        {
            if (this.authorizedUser != null)
            {
                return await this.userCourseService.GetAllCourseInProgress(this.authorizedUser.User.Id);
            }

            return null;
        }

        public async Task<IList<Material>> GetMaterialsFromCourseInProgress(int courseId)
        {
            bool existCourseService = await this.courseService.ExistCourse(courseId);

            if (!existCourseService)
            {
                return null;
            }

            int userCourseId = this.userCourseService.GetUserCourse(this.authorizedUser.User.Id, courseId).Id;
            return await this.userCourseMaterialSqlService.GetNotPassedMaterialsFromCourseInProgress(userCourseId);
        }

        public async Task<IList<Skill>> GetSkillsFromCourseInProgress(int courseId)
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
                this.userRepository.Update(user);
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

        public async Task<IList<Course>> GetAllPassedCourseFromUser()
        {
            if (this.authorizedUser != null)
            {
                return await this.userCourseService.GetAllPassedCourse(this.authorizedUser.User.Id);
            }

            return null;
        }
    }
}
