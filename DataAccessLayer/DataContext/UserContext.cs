using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Entities;
using DataAccessLayer.Serialization;

namespace DataAccessLayer.DataContext
{
    public class UserContext
    {
        public UserSerialization<User> Users = new UserSerialization<User>();
    }
}
