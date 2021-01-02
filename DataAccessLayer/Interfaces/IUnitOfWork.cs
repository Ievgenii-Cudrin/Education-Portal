using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Material> Materials { get; }
        IRepository<Skill> Skills { get; }
        IRepository<Course> Courses { get; }
    }
}
