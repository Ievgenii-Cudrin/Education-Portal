namespace EducationPortalConsoleApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using DataAccessLayer.Repositories;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Comparers;
    using Entities;

    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Course> courseRepository;
        private IAuthorizedUser authorizedUser;
        private ICourseComparerService courseComparer;

        public UserService(
            IEnumerable<IRepository<User>> uRepositories,
            IEnumerable<IRepository<Course>> courseRepositories,
            IAuthorizedUser authUser,
            ICourseComparerService courseComparer)
        {
            this.userRepository = uRepositories.FirstOrDefault(t => t.GetType() == typeof(RepositoryXml<User>));
            this.courseRepository = courseRepositories.FirstOrDefault(t => t.GetType() == typeof(RepositoryXml<Course>));
            this.authorizedUser = authUser;
            this.courseComparer = courseComparer;
        }

        public bool CreateUser(User user)
        {
            bool uniqueEmail = user != null && !this.userRepository.GetAll().Any(x => x.Email.ToLower().Equals(user.Email.ToLower()));

            if (uniqueEmail)
            {
                this.userRepository.Add(user);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool UpdateUser(User userToUpdate)
        {
            User user = this.userRepository.Get(userToUpdate.Id);

            if (user == null)
            {
                return false;
            }
            else
            {
                user.Name = userToUpdate.Name;
                user.Password = userToUpdate.Password;
                user.Email = userToUpdate.Email;
                user.PhoneNumber = userToUpdate.PhoneNumber;
                this.userRepository.Update(user);
            }

            return true;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this.userRepository.GetAll();
        }

        public bool Delete(int id)
        {
            User user = this.userRepository.Get(id);

            if (user == null)
            {
                return false;
            }
            else
            {
                this.userRepository.Delete(id);
            }

            return true;
        }

        public bool AddSkill(Skill skill)
        {
            if (skill == null)
            {
                return false;
            }

            bool skillExistInUser = this.authorizedUser.User.Skills.Any(x => x.Name.ToLower() == skill.Name.ToLower());

            if (!skillExistInUser)
            {
                this.authorizedUser.User.Skills.Add(skill);
                this.userRepository.Update(this.authorizedUser.User);
                return true;
            }
            else
            {
                foreach (var skillUser in this.authorizedUser.User.Skills)
                {
                    if (skillUser.Name == skill.Name)
                    {
                        skillUser.CountOfPoint++;
                    }
                }

                this.userRepository.Update(this.authorizedUser.User);
                return true;
            }
        }

        public IEnumerable<Skill> GetUserSkills()
        {
            return this.userRepository.Get(this.authorizedUser.User.Id).Skills;
        }

        public bool AddCourseInProgress(int id)
        {
            Course course = this.courseRepository.Get(id);

            if (course != null)
            {
                this.authorizedUser.User.CoursesInProgress.Add(course);
                this.userRepository.Update(this.authorizedUser.User);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddCourseToPassed(int id)
        {
            Course course = this.courseRepository.Get(id);

            if (course != null)
            {
                this.authorizedUser.User.CoursesInProgress.ToList().RemoveAll(x => x.Id == id);
                this.authorizedUser.User.CoursesPassed.Add(course);
                this.userRepository.Update(this.authorizedUser.User);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateValueOfPassMaterialInProgress(int courseId, int materialId)
        {
            try
            {
                // find course in progress list and find material from this course, set true
                this.authorizedUser.User.CoursesInProgress.Where(x => x.Id == courseId).FirstOrDefault().Materials.Where(x => x.Id == materialId).FirstOrDefault().IsPassed = true;
                this.userRepository.Update(this.authorizedUser.User);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Course> GetListWithCoursesInProgress()
        {
            return this.authorizedUser.User.CoursesInProgress.ToList();
        }

        public List<Material> GetMaterialsFromCourseInProgress(int id)
        {
            return this.authorizedUser.User.CoursesInProgress.Where(x => x.Id == id).FirstOrDefault().Materials.ToList();
        }

        public List<Skill> GetSkillsFromCourseInProgress(int id)
        {
            return this.authorizedUser.User.CoursesInProgress.Where(x => x.Id == id).FirstOrDefault().Skills.ToList();
        }

        public List<Course> GetAvailableCoursesForUser()
        {
            return this.courseRepository.GetAll().Except(this.authorizedUser.User.CoursesPassed, this.courseComparer.CourseComparer).ToList();
        }

        public List<Skill> GetAllUserSkills()
        {
            return this.authorizedUser.User.Skills.ToList();
        }

        public void UpdateCourseInProgress(int courseInProgressNotFinishId, List<Material> updatedMaterials)
        {
            if (updatedMaterials != null)
            {
                this.authorizedUser.User.CoursesInProgress.Where(x => x.Id == courseInProgressNotFinishId).FirstOrDefault().Materials = updatedMaterials;
                this.userRepository.Update(this.authorizedUser.User);
            }
        }

        public bool ExistEmail(Expression<Func<User, bool>> predicat)
        {
            return this.userRepository.Exist(predicat);
        }

        public List<Course> GetAllPassedCourseFromUser()
        {
            return this.authorizedUser.User.CoursesPassed.ToList();
        }
    }
}
