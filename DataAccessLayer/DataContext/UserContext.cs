using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Entities;

namespace DataAccessLayer.DataContext
{
    public class UserContext
    {
        public List<User> Users { get; set; }
    }
}
