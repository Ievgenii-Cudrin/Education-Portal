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

            //Add inner element text
            
            userElem.AppendChild(nameElem.AppendChild(xDoc.CreateTextNode(user.Name)));
            userElem.AppendChild(emailElem.AppendChild(xDoc.CreateTextNode(user.Email)));
            userElem.AppendChild(phoneElem.AppendChild(xDoc.CreateTextNode(user.PhoneNumber)));

            xRoot.AppendChild(userElem);
            xDoc.Save("person.xml");
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            //Load document
            XDocument xdoc = XDocument.Load("person.xml");

            //Find all users element
            foreach (XElement phoneElement in xdoc.Element("rootElement").Elements("user"))
            {
                //Find inner elemets
                XElement nameAttribute = phoneElement.Element("Name");
                XElement emailElement = phoneElement.Element("Email");
                XElement phoneNumberElement = phoneElement.Element("PhoneNumber");

                if (nameAttribute != null && emailElement != null && phoneNumberElement != null)
                {
                    //Create user from xml
                    User user = new User()
                    {
                        Name = nameAttribute.Value,
                        Email = emailElement.Value,
                        PhoneNumber = phoneNumberElement.Value
                    };

                    users.Add(user);
                }
            }
            return users;
        }
    }
}
