using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System.Linq;
using System;
using System.Collections.Generic;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;

namespace EducationPortalConsoleApp.Services
{
    public class UserService : IUserService
    {
        IRepository<User> userRepository;
        IRepository<Course> courseRepository;
        static User authorizedUser;

        public UserService(IRepository<User> repository, IRepository<Course> courseRepository)
        {
            this.userRepository = repository;
            this.courseRepository = courseRepository;
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
            //check email, may be we have this email
            bool uniqueEmail = user != null ? !userRepository.GetAll().Any(x => x.Email.ToLower().Equals(user.Email.ToLower())) : false;
            
            //if unique emaeil => create new user, otherwise user == null
            if (uniqueEmail)
            {
                userRepository.Create(user);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool VerifyUser(string name, string password)
        {
            User user = userRepository.GetAll().Where(x => x.Name.ToLower() == name.ToLower() && x.Password == password).FirstOrDefault();

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
            //Think about this method
            authorizedUser = null;
            return true;
        }

        public bool UpdateUser(User userToUpdate)
        {
            User user = userRepository.Get(authorizedUser.Id);

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
                userRepository.Update(user);
            }

            return true;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userRepository.GetAll();
        }

        public bool Delete(int id)
        {
            User user = userRepository.Get(id);

            if (user == null)
            {
                return false;
            }
            else
            {
                userRepository.Delete(id);
            }

            return true;
        }

        public bool AddSkill(Skill skill)
        {
            if(skill == null)
            {
                return false;
            }

            bool skillExistInUser = authorizedUser.Skills.Any(x => x.Name.ToLower() == skill.Name.ToLower());

            if (!skillExistInUser)
            {
                authorizedUser.Skills.Add(skill);
                userRepository.Update(authorizedUser);
                return true;
            }
            else
            {
                foreach(var skillUser in authorizedUser.Skills)
                {
                    if(skillUser.Name == skill.Name)
                    {
                        skillUser.CountOfPoint++;
                    }
                }
                userRepository.Update(authorizedUser);
                return true;
            }
        }
        public IEnumerable<Skill> GetUserSkills()
        {
            return userRepository.Get(authorizedUser.Id).Skills;
        }

        public bool AddCourseInProgress(int id)
        {
            Course course = courseRepository.Get(id);
            if (course != null)
            {
                authorizedUser.CoursesInProgress.Add(course);
                userRepository.Update(authorizedUser);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCourseFromProgress(int id)
        {
            Course course = courseRepository.Get(id);
            if (course != null)
            {
                authorizedUser.CoursesInProgress.RemoveAll(x => x.Id == id);
                userRepository.Update(authorizedUser);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddCourseToPassed(int id)
        {
            Course course = courseRepository.Get(id);
            if (course != null)
            {
                authorizedUser.CoursesPassed.Add(course);
                userRepository.Update(authorizedUser);
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
                authorizedUser.CoursesInProgress.Where(x => x.Id == courseId).FirstOrDefault().Materials.Where(x => x.Id == materialId).FirstOrDefault().IsPassed = true;
                userRepository.Update(authorizedUser);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<Course> GetListWithCoursesInProgress()
        {
            return authorizedUser.CoursesInProgress;
        }

        public List<Material> GetMaterialsFromCourseInProgress(int id)
        {
            return authorizedUser.CoursesInProgress.Where(x => x.Id == id).FirstOrDefault().Materials.ToList();
        }

        public List<Skill> GetSkillsFromCourseInProgress(int id)
        {
            return authorizedUser.CoursesInProgress.Where(x => x.Id == id).FirstOrDefault().Skills.ToList();
        }
    }
}
