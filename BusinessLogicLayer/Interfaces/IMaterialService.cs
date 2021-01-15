using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMaterialService : IDeleteEntity
    {
        public bool CreateVideo(string name, string link, int quality, int duration);

        public bool CreateArticle(string name, string site, DateTime publicationDate);

        public bool CreateBook(string name, string author, int countOfPages);

        public bool UpdateVideo(int id, string name, string link, int quality, int duration);

        public bool UpdateArticle(int id, string name, string site, DateTime publicationDate);

        public bool UpdateBook(int id, string name, string author, int countOfPages);

        public IEnumerable<string> GetAllMaterials();
    }
}
