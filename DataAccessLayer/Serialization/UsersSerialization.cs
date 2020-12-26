using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;

namespace DataAccessLayer.Serialization
{
    public class UserSerialization
    {
        XmlDocument xmlDocument = new XmlDocument();
        XDocument xDocument;
        public UserSerialization()
        {
            xmlDocument.Load("person.xml");
            xDocument = XDocument.Load("person.xml");
        }

        public void AddObjectToXml(User user)
        {
            XmlElement xRoot = xmlDocument.DocumentElement;

            //Create elements
            XmlElement userElem = xmlDocument.CreateElement("user");

            XmlElement nameElem = xmlDocument.CreateElement("Name");
            XmlElement emailElem = xmlDocument.CreateElement("Email");
            XmlElement phoneElem = xmlDocument.CreateElement("PhoneNumber");
            XmlAttribute idAttribute = xmlDocument.CreateAttribute("id");

            // Create inner element text
            XmlText nameText = xmlDocument.CreateTextNode(user.Name);
            XmlText emailText = xmlDocument.CreateTextNode(user.Email);
            XmlText phoneText = xmlDocument.CreateTextNode(user.PhoneNumber);
            XmlText idText = xmlDocument.CreateTextNode(GenareteId().ToString());

            //Append child elements
            idAttribute.AppendChild(idText);
            nameElem.AppendChild(nameText);
            emailElem.AppendChild(emailText);
            phoneElem.AppendChild(phoneText);
            userElem.Attributes.Append(idAttribute);

            userElem.AppendChild(nameElem);
            userElem.AppendChild(emailElem);
            userElem.AppendChild(phoneElem);

            xRoot.AppendChild(userElem);
            xmlDocument.Save("person.xml");
        }

        public IEnumerable<User> GetAllUsersFromXml()
        {
            List<User> users = new List<User>();
            
            //Find all users element
            foreach (XElement userElement in xDocument.Element("rootElement").Elements("user"))
            {
                //Find inner elemets
                XElement nameAttribute = userElement.Element("Name");
                XElement emailElement = userElement.Element("Email");
                XElement phoneNumberElement = userElement.Element("PhoneNumber");

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

        public IEnumerable<User> Find(Func<User, Boolean> predicate)
        {
            return GetAllUsersFromXml().Where(predicate).ToList();
        }

        public User GetSingleUserFromXml(int id)
        {
            //XmlNode childnode = xmlDocument.DocumentElement.SelectSingleNode($"user[@id='{id.ToString()}']");
            User user = new User();
            foreach (XElement userElement in xDocument.Element("rootElement").Elements("user"))
            {
                XAttribute nameAttribute = userElement.Attribute("id");

                if (nameAttribute != null && Convert.ToInt32(nameAttribute.Value) == id)
                {
                    user.Name = userElement.Element("Name").Value;
                    user.Email = userElement.Element("Email").Value;
                    user.PhoneNumber = userElement.Element("PhoneNumber").Value;
                    break;
                }
            }

            return user;
        }

        public void DeleteUserFromXml(int id)
        {
            //Find node by name
            //XmlNode nodeToDelete = xmlDocument.DocumentElement.SelectSingleNode($"user[Name='{name}']");

            //Find noed by id
            XmlNode nodeToDelete = xmlDocument.DocumentElement.SelectSingleNode($"user[@id='{id.ToString()}']");
            if (nodeToDelete != null)
            {
                xmlDocument.DocumentElement.RemoveChild(nodeToDelete);
                xmlDocument.Save("person.xml");
            }
        }

        private int GenareteId()
        {
            XDocument xDoc = XDocument.Load("person.xml");
            //Get id from last 
            XElement lastPost = (XElement)xDoc.Root.LastNode;
            int Id = Convert.ToInt32(lastPost.Attribute("id").Value);

            return ++Id;
        }
    }
}
