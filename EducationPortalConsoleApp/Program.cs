using DataAccessLayer.Entities;
using DataAccessLayer.Serialization;
using EducationPortalConsoleApp.Services;
using System;
using System.Collections.Generic;

namespace EducationPortalConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerialization<User> serialize = new XmlSerialization<User>();
            XmlSerialization<Skill> skillSerialize = new XmlSerialization<Skill>();
            XmlSerialization<Material> materialSerialize = new XmlSerialization<Material>();

            User user = new User()
            {
                Id = "728",
                Name = "Jeffrey Richter obj 728",
                Email = "728-Kto-to-noviy@gmail.com",
                PhoneNumber = "321654"
            };

            //serialize.Add(user);

            //User user2 = serialize.Get(722);

            //IEnumerable<User> users = serialize.GetAll();

            //serialize.Delete(722);

            //serialize.UpdateObject(user);

            Skill skill = new Skill()
            {
                Name = ".Net",
                CountOfPoint = 0
            };

            //skillSerialize.Add(skill);

            Material inkapsulationVideo = new Video()
            {
                Duration = "9:06",
                Link = "https://www.youtube.com/watch?v=C0FNqM7hsao&ab_channel=%23SimpleCode",
                Name = "Incapsulation",
                Quality = "480p"
            };

            materialSerialize.Add(inkapsulationVideo);

            Material polymorphism = new Article()
            {
                Name = "Polymorphism",
                PublicationDate = new DateTime(2016, 11, 28),
                Site = "https://metanit.com/sharp/tutorial/3.19.php"
            };

            materialSerialize.Add(polymorphism);



            //new ProgramService().StartApp();

            Console.ReadLine();
        }
    }
}
