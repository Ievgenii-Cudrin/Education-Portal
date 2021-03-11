using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EducationPortal.XmlDataBase.Helpres
{
    public static class XmlMaker
    {
        public static int GenareteId(DirectoryInfo directory)
        {
            int id = 0;
            try
            {
                id = directory.GetFiles("*.xml").OrderBy(x => x.Name).Select(x => x.Name).Select(x => Convert.ToInt32(Regex.Match(x, @"\d+").Value)).Max();
            }
            catch
            {
                return id;
            }

            return ++id;
        }

        public static void EncodePasswordAndSetToObject<T>(ref T objectToUpdate)
        {
            string encodePassword = PasswordEncoder.EncodeToBase64String(typeof(T).GetProperty("Password").GetValue(objectToUpdate).ToString());
            typeof(T).GetProperty("Password").SetValue(objectToUpdate, encodePassword);
        }

        public static void DecodePasswordAndSetToObject<T>(ref T objToXml)
        {
            string decodePassword = PasswordDecoder.DecodeToBase64String(typeof(T).GetProperty("Password").GetValue(objToXml).ToString());
            typeof(T).GetProperty("Password").SetValue(objToXml, decodePassword);
        }
    }
}
