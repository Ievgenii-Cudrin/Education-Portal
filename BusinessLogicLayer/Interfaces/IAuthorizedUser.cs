namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataAccessLayer.Entities;

    public interface IAuthorizedUser
    {
        public User User { get; }
    }
}
