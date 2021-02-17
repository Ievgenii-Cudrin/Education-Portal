namespace EducationPortal.BLL.ServicesSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.DAL.Repositories;
    using Entities;

    public class UserSqlService : IUserService
    {
        private static User authorizedUser;
        private readonly IRepository<User> userRepository;

        public UserSqlService(IEnumerable<IRepository<User>> uRepo)
        {
            this.userRepository = uRepo.FirstOrDefault(t => t.GetType() == typeof(RepositorySql<User>));
        }

        public User AuthorizedUser
        {
            get
            {
                return authorizedUser;
            }
        }

        public bool AddCourseInProgress(int id)
        {
            throw new NotImplementedException();
        }

        public bool AddCourseToPassed(int id)
        {
            throw new NotImplementedException();
        }

        public bool AddSkill(Skill skill)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool DeleteCourseFromProgress(int id)
        {
            throw new NotImplementedException();
        }

        public List<Skill> GetAllUserSkills()
        {
            throw new NotImplementedException();
        }

        public List<Course> GetAvailableCoursesForUser()
        {
            throw new NotImplementedException();
        }

        public List<Course> GetListWithCoursesInProgress()
        {
            throw new NotImplementedException();
        }

        public List<Material> GetMaterialsFromCourseInProgress(int id)
        {
            throw new NotImplementedException();
        }

        public List<Skill> GetSkillsFromCourseInProgress(int id)
        {
            throw new NotImplementedException();
        }

        public bool LogOut()
        {
            authorizedUser = null;
            return true;
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
            throw new NotImplementedException();
        }

        public bool VerifyUser(string name, string password)
        {
            User user = this.userRepository.Get(x => x.Name.ToLower() == name.ToLower() && x.Password == password).FirstOrDefault();

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

        public bool ExistEmail(Expression<Func<User, bool>> predicat)
        {
            return this.userRepository.Exist(predicat);
        }
    }
}
