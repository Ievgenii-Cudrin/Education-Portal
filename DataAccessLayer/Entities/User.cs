using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
