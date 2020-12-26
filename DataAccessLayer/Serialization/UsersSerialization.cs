using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataAccessLayer.Serialization
{
    public class UsersSerialization
    {
        XmlSerializer writer = new XmlSerializer(typeof(User));
        public void AddObjectToXml(User user)
        {
            //Load document
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("person.xml");

            XmlElement xRoot = xDoc.DocumentElement;

            //Create elements
            XmlElement userElem = xDoc.CreateElement("user");

            XmlElement nameElem = xDoc.CreateElement("Name");
            XmlElement emailElem = xDoc.CreateElement("Email");
            XmlElement phoneElem = xDoc.CreateElement("PhoneNumber");

            // Create inner element text
            XmlText nameText = xDoc.CreateTextNode(user.Name);
            XmlText emailText = xDoc.CreateTextNode(user.Email);
            XmlText phoneText = xDoc.CreateTextNode(user.PhoneNumber);

            nameElem.AppendChild(nameText);
            emailElem.AppendChild(emailText);
            phoneElem.AppendChild(phoneText);

            userElem.AppendChild(nameElem);
            userElem.AppendChild(emailElem);
            userElem.AppendChild(phoneElem);

            xRoot.AppendChild(userElem);
            xDoc.Save("person.xml");
        }

        public User[] GetAllUsers()
        {

            return null;
        }
    }
}
