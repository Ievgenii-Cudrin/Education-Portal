﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using EducationPortal.XmlDataBase.Helpres;
using XmlDataBase.Interfaces;

namespace XmlDataBase.Serialization
{
    public class XmlSet<T> : IXmlSet<T>
        where T : class
    {
        private Type type;
        private XmlSerializer serializer;
        private DirectoryInfo directory;
        private bool objectHasPasswordProperty;

        public XmlSet()
        {
            this.type = typeof(T);
            this.serializer = new XmlSerializer(typeof(T));
            this.directory = new DirectoryInfo($"{this.type.Name}");
            this.objectHasPasswordProperty = typeof(T).GetProperties().Any(x => x.Name == "Password");
        }

        public void Add(T objToXml)
        {
            // Generate ID and add to object
            int id = XmlMaker.GenareteId(this.directory);
            typeof(T).GetProperty("Id").SetValue(objToXml, id);

            // decode password
            if (this.objectHasPasswordProperty)
            {
                XmlMaker.DecodePasswordAndSetToObject(ref objToXml);
            }

            // Create directory if not exist
            Directory.CreateDirectory($"{this.type.Name}");

            // add object to xml
            using (FileStream fs = new FileStream($"{this.type.Name}/{this.type.Name}{id}.xml", FileMode.Create))
            {
                this.serializer.Serialize(fs, objToXml);
            }
        }

        public IEnumerable<T> GetAll()
        {
            List<T> entities = new List<T>();
            Directory.CreateDirectory($"{this.type.Name}");

            // Get all xml files from our type directory
            FileInfo[] files = this.directory.GetFiles("*.xml");

            if (files == null)
            {
                return entities;
            }

            foreach (var file in files)
            {
                using (FileStream fs = new FileStream(file.FullName, FileMode.Open))
                {
                    // deserialize object
                    T objFromXml = (T)this.serializer.Deserialize(fs);

                    if (this.objectHasPasswordProperty)
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
            // get file by id
            FileInfo file = this.directory.GetFiles($"{type.Name}{id}.xml").FirstOrDefault();

            if (file == null)
            {
                return null;
            };

            using (FileStream fs = new FileStream($"{this.type.Name}/{file.Name}", FileMode.Open))
            {
                // deserialize object
                objectFromXml = (T)this.serializer.Deserialize(fs);

                if (this.objectHasPasswordProperty)
                {
                    // encode password
                    XmlMaker.EncodePasswordAndSetToObject(ref objectFromXml);
                }
            }

            return objectFromXml;

        }

        public void Delete(int id)
        {
            FileInfo file = this.directory.GetFiles($"{this.type.Name}{id}.xml").FirstOrDefault();

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

            using (FileStream fs = new FileStream($"{this.type.Name}/{this.type.Name}{typeof(T).GetProperty("Id").GetValue(objectToUpdate)}.xml", FileMode.Create))
            {
                if (this.objectHasPasswordProperty)
                {
                    // decode password
                    XmlMaker.DecodePasswordAndSetToObject(ref objectToUpdate);
                }

                // serialize
                this.serializer.Serialize(fs, objectToUpdate);

                if (this.objectHasPasswordProperty)
                {
                    // encode password
                    XmlMaker.EncodePasswordAndSetToObject(ref objectToUpdate);
                }
            }
        }
    }
}
