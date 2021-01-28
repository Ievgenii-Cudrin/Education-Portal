using EducationPortal.XmlDataBase.Helpres;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;
using XmlDataBase.Interfaces;

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
            int id = XmlMaker.GenareteId(directory);
            typeof(T).GetProperty("Id").SetValue(objToXml, id);

            //decode password
            if (objectHasPasswordProperty)
            {
                XmlMaker.DecodePasswordAndSetToObject(ref objToXml);
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

            if(files == null)
            {
                return entities;
            }

            foreach (var file in files)
            {
                using (FileStream fs = new FileStream(file.FullName, FileMode.Open))
                {
                    //deserialize object
                    T objFromXml = (T)serializer.Deserialize(fs);

                    if (objectHasPasswordProperty)
                    {
                        XmlMaker.EncodePasswordAndSetToObject(ref objFromXml);
                    }

                    entities.Add(objFromXml);
                }
            }

            return entities;
        }

        public T Get(int id)
        {
            T objectFromXml;
            //get file by id
            FileInfo file = directory.GetFiles($"{type.Name}{id}.xml").FirstOrDefault();

            if (file == null)
            {
                return null;
            };

            using (FileStream fs = new FileStream($"{type.Name}/{file.Name}", FileMode.Open))
            {
                //deserialize object
                objectFromXml = (T)serializer.Deserialize(fs);

                if (objectHasPasswordProperty)
                {
                    //encode password
                    XmlMaker.EncodePasswordAndSetToObject(ref objectFromXml);
                }
            }

            return objectFromXml;

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

        public void Update(T objectToUpdate)
        {
            Thread.Sleep(200);

            using (FileStream fs = new FileStream($"{type.Name}/{type.Name}{typeof(T).GetProperty("Id").GetValue(objectToUpdate)}.xml", FileMode.Create))
            {
                if (objectHasPasswordProperty)
                {
                    //decode password
                    XmlMaker.DecodePasswordAndSetToObject(ref objectToUpdate);
                }
                
                //serialize
                serializer.Serialize(fs, objectToUpdate);

                if (objectHasPasswordProperty)
                {
                    //encode password
                    XmlMaker.EncodePasswordAndSetToObject(ref objectToUpdate);
                }
            }
        }
    }
}
