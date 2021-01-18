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

namespace XmlDataBase.Serialization
{
    public class XmlSet<T> : IXmlSet<T> where T : class  //XmlSerialization
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
            typeof(T).GetProperty("Id").SetValue(objToXml, id);
            Directory.CreateDirectory($"{type.Name}");

            using(FileStream fs = new FileStream($"{type.Name}/{type.Name}{id}.xml", FileMode.Create))
            {
                serializer.Serialize(fs, objToXml);
            }
        }

        public IEnumerable<T> GetAll()
        {
            List<T> users = new List<T>();
            Directory.CreateDirectory($"{type.Name}");
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
            FileInfo file= directory.GetFiles($"{type.Name}{id}.xml").FirstOrDefault();
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
            using (FileStream fs = new FileStream($"{type.Name}/{type.Name}{typeof(T).GetProperty("Id").GetValue(objectToUpdate)}.xml", FileMode.Open))
            {
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
            catch (Exception ex)
            {
                return id;
            }
            
            return ++id;
        }
    }
}
