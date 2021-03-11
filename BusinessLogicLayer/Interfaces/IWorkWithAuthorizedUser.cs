using DataAccessLayer.Entities;

namespace EducationPortal.BLL.Interfaces
{

    public interface IWorkWithAuthorizedUser : IAuthorizedUser
    {
        public void SetUser(User user);

        public void CleanUser();
    }
}
