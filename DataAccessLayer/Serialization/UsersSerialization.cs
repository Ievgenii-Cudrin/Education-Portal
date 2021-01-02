﻿using DataAccessLayer.Entities;
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

        public T GetSingleUserFromXml(int id)
        {
            T objectFromXml;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream($"{type.Name}/{type.Name}{id}.xml", FileMode.Open);
            objectFromXml = (T)serializer.Deserialize(fs);
            return objectFromXml;
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
            DirectoryInfo d = new DirectoryInfo($"{type.Name}");//Assuming Test is your Folder
            FileInfo File = d.GetFiles("*.xml").LastOrDefault(); //Getting Text files
            int id = Convert.ToInt32(Regex.Match(File.Name, @"\d+").Value);

            return ++id;
        }
    }
}
