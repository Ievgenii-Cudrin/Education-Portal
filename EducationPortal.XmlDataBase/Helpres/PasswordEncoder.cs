using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.XmlDataBase.Helpres
{
    public static class PasswordEncoder
    {
        public static string EncodeToBase64String(string base64String)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
        }
    }
}
