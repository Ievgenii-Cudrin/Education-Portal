using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.XmlDataBase.Helpres
{
    public static class PasswordDecoder
    {
        public static string DecodeToBase64String(string passwordToDecode)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(passwordToDecode));
        }
    }
}
