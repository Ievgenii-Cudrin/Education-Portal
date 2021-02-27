namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Entities;
    using Entities;

    public class UserSqlService : IUserService
    {
        private IRepository<User> userRepository;
        private ICourseService courseService;
        private IAuthorizedUser authorizedUser;
        private IUserCourseSqlService userCourseService;
        private IUserCourseMaterialSqlService userCourseMaterialSqlService;
        private IUserMaterialSqlService userMaterialSqlService;
        private IUserSkillSqlService userSkillSqlService;
        private ICourseSkillService courseSkillService;

        public UserSqlService(
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

        public bool AddCourseInProgress(int courseId)
        {
            if (this.authorizedUser != null &&
                this.courseService.ExistCourse(courseId))
            {
                this.userCourseService.AddCourseToUser(this.authorizedUser.User.Id, courseId);
                return true;
            }

            return false;
        }

        public bool AddCourseToPassed(int courseId)
        {
            this.userCourseService.SetPassForUserCourse(this.authorizedUser.User.Id, courseId);
            return true;
        }

        public bool AddSkill(Skill skill)
        {
            if (skill != null)
            {
                this.userSkillSqlService.AddSkillToUser(this.authorizedUser.User.Id, skill.Id);
                return true;
            }

            return false;
        }

        public bool CreateUser(User user)
        {
            bool uniqueEmail = user != null && !this.userRepository.Exist(x => x.Email.ToLower().Equals(user.Email.ToLower()));

            if (uniqueEmail)
            {
                this.userRepository.Add(user);
                this.userRepository.Save();
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            if (this.userRepository.Exist(x => x.Id == id))
            {
                this.userRepository.Delete(id);
                this.userRepository.Save();
                return true;
            }

            return false;
        }

        public List<Skill> GetAllUserSkills()
        {
            if (this.authorizedUser != null)
            {
                return this.userSkillSqlService.GetAllSkillInUser(this.authorizedUser.User.Id);
            }

            return null;
        }

        public List<Course> GetAvailableCoursesForUser()
        {
            if (this.authorizedUser != null)
            {
                var coursesInProgressAndPassed = this.userCourseService.GetAllPassedAndProgressCoursesForUser(this.authorizedUser.User.Id);
                return this.courseService.AvailableCourses(coursesInProgressAndPassed);
            }

            return null;
        }

        public List<Course> GetListWithCoursesInProgress()
        {
            if (this.authorizedUser != null)
            {
                return this.userCourseService.GetAllCourseInProgress(this.authorizedUser.User.Id);
            }

            return null;
        }

        public List<Material> GetMaterialsFromCourseInProgress(int courseId)
        {
            if (!this.courseService.ExistCourse(courseId))
            {
                return null;
            }

            int userCourseId = this.userCourseService.GetUserCourse(this.authorizedUser.User.Id, courseId).Id;
            return this.userCourseMaterialSqlService.GetNotPassedMaterialsFromCourseInProgress(userCourseId);
        }

        public List<Skill> GetSkillsFromCourseInProgress(int courseId)
        {
            if (this.courseService.ExistCourse(courseId))
            {
                return this.courseSkillService.GetAllSkillsFromCourse(courseId);
            }

            return null;
        }

        public void UpdateCourseInProgress(int courseInProgressNotFinishId, List<Material> updatedMaterials)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            if (this.userRepository.Exist(x => x.Id == user.Id))
            {
                this.userRepository.Update(user);
                this.userRepository.Save();
                return true;
            }

            return false;

        }

        public bool UpdateValueOfPassMaterialInProgress(int courseId, int materialId)
        {
            int userCourseId = this.userCourseService.GetUserCourse(this.authorizedUser.User.Id, courseId).Id;
            bool success = this.userCourseMaterialSqlService.SetPassToMaterial(userCourseId, materialId);

            if (success)
            {
                // Add pass material to user
                this.userMaterialSqlService.AddMaterialToUser(this.authorizedUser.User.Id, materialId);
                return true;
            }

            return false;
        }

        public bool ExistEmail(Expression<Func<User, bool>> predicat)
        {
            return this.userRepository.Exist(predicat);
        }

        public List<Course> GetAllPassedCourseFromUser()
        {
            if (this.authorizedUser != null)
            {
                return this.userCourseService.GetAllPassedCourse(this.authorizedUser.User.Id);
            }

            return null;
        }
    }
}
