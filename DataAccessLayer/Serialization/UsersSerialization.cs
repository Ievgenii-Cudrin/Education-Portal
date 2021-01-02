using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataAccessLayer.Serialization
{
    public class UserSerialization<T>
    {
        XmlDocument xmlDocument;
        XDocument xDocument;
        Type type;

        static string pathToDocument = "person.xml";
        public UserSerialization()
        {
            xmlDocument = new XmlDocument();
            xmlDocument.Load(pathToDocument);
            xDocument = XDocument.Load(pathToDocument);
            type = typeof(T);
        }

        public void AddObjectToXml(T objToXml)
        {
            System.IO.Directory.CreateDirectory($"{type.Name}");
            XmlSerializer serializer = new XmlSerializer(typeof(T));                             //Не нужен второй параметр. Проверить
            FileStream fs = new FileStream($"{type.Name}/{type.Name}{GenareteId()}.xml", FileMode.Create);
            serializer.Serialize(fs, objToXml);
            fs.Close();
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

        public User GetSingleUserFromXml(int id)
        {
            //XmlNode childnode = xmlDocument.DocumentElement.SelectSingleNode($"user[@id='{id.ToString()}']");
            User user = new User();
            foreach (XElement userElement in xDocument.Element("rootElement").Elements("user"))
            {
                XAttribute nameAttribute = userElement.Attribute("id");

                if (nameAttribute != null && Convert.ToInt32(nameAttribute.Value) == id)
                {
                    user.Id = userElement.Attribute("id").Value;
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
                xmlDocument.Save(pathToDocument);
            }
        }

        public void UpdateObject(User user)
        {
            //Sorting out xml elements
            foreach (XElement userElement in xDocument.Element("rootElement").Elements("user").ToList())
            {
                XAttribute nameAttribute = userElement.Attribute("id");
                
                //Find element by id
                if (nameAttribute != null && nameAttribute.Value == user.Id)
                {
                    //Update data in node
                    userElement.Element("Name").Value = user.Name;
                    userElement.Element("Email").Value = user.Email;
                    userElement.Element("PhoneNumber").Value = user.PhoneNumber;
                    xDocument.Save(pathToDocument);
                    break;
                }
            }
        }

        public void SaveChanges() => xDocument.Save(pathToDocument);

        public IEnumerable<User> Find(Func<User, Boolean> predicate) => GetAllUsersFromXml().Where(predicate).ToList();

        private int GenareteId()
        {
            XDocument xDoc = XDocument.Load(pathToDocument);
            //Get id from last 
            XElement lastPost = (XElement)xDoc.Root.LastNode;
            int Id = Convert.ToInt32(lastPost.Attribute("id").Value);

            return ++Id;
        }
    }
}
