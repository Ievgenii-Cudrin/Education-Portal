using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;
using System.Text.RegularExpressions;
using XmlDataBase.Interfaces;
using System.Threading;
using EducationPortal.XmlDataBase.Helpres;
using System.Reflection;

namespace XmlDataBase.Serialization
{
    public class XmlSet<T> : IXmlSet<T> where T : class  //XmlSerialization
    {
        Type type;
        XmlSerializer serializer;
        DirectoryInfo directory;
        bool objectHasPasswordProperty;

        public XmlSet() 
        { 
            type = typeof(T);
            serializer = new XmlSerializer(typeof(T));
            directory = new DirectoryInfo($"{type.Name}");
            objectHasPasswordProperty = typeof(T).GetProperties().Any(x => x.Name == "Password");
        }

        public void Add(T objToXml)
        {
            //Generate ID and add to object
            int id = GenareteId();
            typeof(T).GetProperty("Id").SetValue(objToXml, id);

            //decode password
            if (objectHasPasswordProperty)
            {
                string decodePassword = PasswordDecoder.DecodeToBase64String(typeof(T).GetProperty("Password").GetValue(objToXml).ToString());
                typeof(T).GetProperty("Password").SetValue(objToXml, decodePassword);
            }

            //Create directory if not exist
            Directory.CreateDirectory($"{type.Name}");

            //add object to xml
            using (FileStream fs = new FileStream($"{type.Name}/{type.Name}{id}.xml", FileMode.Create))
            {
                serializer.Serialize(fs, objToXml);
            }
        }

        public IEnumerable<T> GetAll()
        {
            List<T> entities = new List<T>();
            Directory.CreateDirectory($"{type.Name}");
            //Get all xml files from our type directory
            FileInfo[] files = directory.GetFiles("*.xml");

            if(files != null)
            {
                foreach (var file in files)
                {
                    using(FileStream fs = new FileStream(file.FullName, FileMode.Open))
                    {
                        //deserialize object
                        T objFromXml = (T)serializer.Deserialize(fs);

                        if (objectHasPasswordProperty)
                        {
                            string encodePassword = PasswordEncoder.EncodeToBase64String(typeof(T).GetProperty("Password").GetValue(objFromXml).ToString());
                            typeof(T).GetProperty("Password").SetValue(objFromXml, encodePassword);
                        }

                        entities.Add(objFromXml);
                    }
                }
            }

            return entities;
        }

        public T Get(int id)
        {
            T objectFromXml;
            //get file by id
            FileInfo file = directory.GetFiles($"{type.Name}{id}.xml").FirstOrDefault();

            if (file != null)
            {
                using (FileStream fs = new FileStream($"{type.Name}/{file.Name}", FileMode.Open))
                {
                    //deserialize object
                    objectFromXml = (T)serializer.Deserialize(fs);

                    if (objectHasPasswordProperty)
                    {
                        //encode password
                        string encodePassword = PasswordEncoder.EncodeToBase64String(typeof(T).GetProperty("Password").GetValue(objectFromXml).ToString());
                        typeof(T).GetProperty("Password").SetValue(objectFromXml, encodePassword);
                    }
                }

                return objectFromXml;
            };
            
            return null;
        }

        public void Delete(int id)
        {
            FileInfo file = directory.GetFiles($"{type.Name}{id}.xml").FirstOrDefault();

            if (file.Exists)
            {
                file.Delete();
            }
            else
            {
                Console.WriteLine("File note found");
            }
        }

        public void UpdateObject(T objectToUpdate)
        {
            Thread.Sleep(200);

            using (FileStream fs = new FileStream($"{type.Name}/{type.Name}{typeof(T).GetProperty("Id").GetValue(objectToUpdate)}.xml", FileMode.Create))
            {
                if (objectHasPasswordProperty)
                {
                    //decode password
                    string decodePassword = PasswordDecoder.DecodeToBase64String(typeof(T).GetProperty("Password").GetValue(objectToUpdate).ToString());
                    typeof(T).GetProperty("Password").SetValue(objectToUpdate, decodePassword);
                }
                
                //serialize
                serializer.Serialize(fs, objectToUpdate);
            }
        }

        //push to another class
        private int GenareteId()
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
    }
}
