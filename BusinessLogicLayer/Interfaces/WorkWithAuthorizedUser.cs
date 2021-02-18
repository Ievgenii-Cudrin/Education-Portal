namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataAccessLayer.Entities;

    public interface WorkWithAuthorizedUser : IAuthorizedUser
    {
        public void SetUser(User user);

        public void CleanUser();
    }
}
