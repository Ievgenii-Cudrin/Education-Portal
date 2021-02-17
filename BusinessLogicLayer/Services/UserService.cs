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
    using EducationPortal.DAL.Repositories;
    using EducationPortal.Domain.Comparers;
    using Entities;

    public class UserService : IUserService
    {
        private static User authorizedUser;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Course> courseRepository;

        public UserService(IEnumerable<IRepository<User>> uRepositories, IEnumerable<IRepository<Course>> courseRepositories)
        {
            this.userRepository = uRepositories.FirstOrDefault(t => t.GetType() == typeof(RepositoryXml<User>));
            this.courseRepository = courseRepositories.FirstOrDefault(t => t.GetType() == typeof(RepositoryXml<Course>));
        }

        public User AuthorizedUser
        {
            get
            {
                return authorizedUser;
            }
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

        public bool VerifyUser(string name, string password)
        {
            User user = this.userRepository.GetAll().Where(x => x.Name.ToLower() == name.ToLower() && x.Password == password).FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                authorizedUser = user;
            }

            return true;
        }

        public bool LogOut()
        {
            authorizedUser = null;
            return true;
        }

        public bool UpdateUser(User userToUpdate)
        {
            User user = this.userRepository.Get(authorizedUser.Id);

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

            bool skillExistInUser = authorizedUser.Skills.Any(x => x.Name.ToLower() == skill.Name.ToLower());

            if (!skillExistInUser)
            {
                authorizedUser.Skills.Add(skill);
                this.userRepository.Update(authorizedUser);
                return true;
            }
            else
            {
                foreach (var skillUser in authorizedUser.Skills)
                {
                    if (skillUser.Name == skill.Name)
                    {
                        skillUser.CountOfPoint++;
                    }
                }

                this.userRepository.Update(authorizedUser);
                return true;
            }
        }

        public IEnumerable<Skill> GetUserSkills()
        {
            return this.userRepository.Get(authorizedUser.Id).Skills;
        }

        public bool AddCourseInProgress(int id)
        {
            Course course = this.courseRepository.Get(id);

            if (course != null)
            {
                authorizedUser.CoursesInProgress.Add(course);
                this.userRepository.Update(authorizedUser);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCourseFromProgress(int id)
        {
            Course course = this.courseRepository.Get(id);

            if (course != null)
            {
                authorizedUser.CoursesInProgress.ToList().RemoveAll(x => x.Id == id);
                this.userRepository.Update(authorizedUser);
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
                authorizedUser.CoursesPassed.Add(course);
                this.userRepository.Update(authorizedUser);
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
                authorizedUser.CoursesInProgress.Where(x => x.Id == courseId).FirstOrDefault().Materials.Where(x => x.Id == materialId).FirstOrDefault().IsPassed = true;
                this.userRepository.Update(authorizedUser);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Course> GetListWithCoursesInProgress()
        {
            return authorizedUser.CoursesInProgress.ToList();
        }

        public List<Material> GetMaterialsFromCourseInProgress(int id)
        {
            return authorizedUser.CoursesInProgress.Where(x => x.Id == id).FirstOrDefault().Materials.ToList();
        }

        public List<Skill> GetSkillsFromCourseInProgress(int id)
        {
            return authorizedUser.CoursesInProgress.Where(x => x.Id == id).FirstOrDefault().Skills.ToList();
        }

        public List<Course> GetAvailableCoursesForUser()
        {
            return this.courseRepository.GetAll().Except(authorizedUser.CoursesPassed, new CourseComparer()).ToList();
        }

        public List<Skill> GetAllUserSkills()
        {
            return authorizedUser.Skills.ToList();
        }

        public void UpdateCourseInProgress(int courseInProgressNotFinishId, List<Material> updatedMaterials)
        {
            if (updatedMaterials != null)
            {
                authorizedUser.CoursesInProgress.Where(x => x.Id == courseInProgressNotFinishId).FirstOrDefault().Materials = updatedMaterials;
                this.userRepository.Update(authorizedUser);
            }
        }

        public bool ExistEmail(Expression<Func<User, bool>> predicat)
        {
            return this.userRepository.Exist(predicat);
        }
    }
}
