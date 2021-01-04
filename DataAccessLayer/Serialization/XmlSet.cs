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
    public class XmlSet<T> where T : class  //XmlSerialization
    {
        Type type;
        XmlSerializer serializer;
        DirectoryInfo directory;

        public XmlSet() 
        { 
            type = typeof(T);
            serializer = new XmlSerializer(typeof(T));
            directory = new DirectoryInfo($"{type.Name}");
        }

        public void Add(T objToXml)
        {
            int id = GenareteId();
            typeof(T).GetProperty("Id").SetValue(objToXml, id.ToString());
            Directory.CreateDirectory($"{type.Name}");
            using(FileStream fs = new FileStream($"{type.Name}/{type.Name}{id}.xml", FileMode.Create))
            {
                serializer.Serialize(fs, objToXml);
            }
        }

        public IEnumerable<T> GetAll()
        {
            List<T> users = new List<T>();

            FileInfo[] files = directory.GetFiles("*.xml");

            if(files != null)
            {
                foreach (var file in files)
                {
                    using(FileStream fs = new FileStream(file.FullName, FileMode.Open))
                    {
                        users.Add((T)serializer.Deserialize(fs));
                    }
                }
            }

            return users;
        }

        public T Get(int id)
        {
            T objectFromXml;
            FileInfo file;

            file = directory.GetFiles($"{type.Name}{id}.xml").FirstOrDefault();
            if (file != null)
            {
                using (FileStream fs = new FileStream($"{type.Name}/{file.Name}", FileMode.Open))
                {
                    objectFromXml = (T)serializer.Deserialize(fs);
                }
                return objectFromXml;
            };
            
            return null;
        }

        public void Delete(int id)
        {
            FileInfo file = directory.GetFiles($"{type.Name}{id}.xml").FirstOrDefault();
            if(file.Exists)
                file.Delete();
            else
                Console.WriteLine("File note found");
        }

        public void UpdateObject(T objectToUpdate)
        {
            using (FileStream fs = new FileStream($"{type.Name}/{type.Name}{typeof(T).GetProperty("Id").GetValue(objectToUpdate)}.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, objectToUpdate);
            }
        }

        private int GenareteId()
        {
            FileInfo file = directory.GetFiles("*.xml").LastOrDefault();
            int id = 0;
            if (file != null)
                id = Convert.ToInt32(Regex.Match(file.Name, @"\d+").Value) + 1;

            return id;
        }
    }
}
