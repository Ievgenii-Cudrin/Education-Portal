using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EducationPortalConsoleApp.InstanceCreator;

namespace EducationPortalConsoleApp.Services
{
    public class MaterialService : IMaterialService
    {
        IRepository<Material> repository = ProviderServiceBLL.Provider.GetRequiredService<IRepository<Material>>();

        public bool CreateVideo(string name, string link, int quality, int duration)
        {
            //check name, may be we have this skill
            bool uniqueName = !repository.GetAll().Any(x => x.Name.ToLower().Equals(name.ToLower()));
            //if name is unique => create new skill, otherwise skill == null
            Video video = uniqueName ? VideoInstanceCreator.CreateVideo(name, link, quality, duration) : null;

            if (video != null)
            {
                repository.Create(video);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool CreateArticle(string name, string site, DateTime publicationDate)
        {
            //check name, may be we have this skill
            bool uniqueName = !repository.GetAll().Any(x => x.Name.ToLower().Equals(name.ToLower()));
            //if name is unique => create new skill, otherwise skill == null
            Article article = uniqueName ? ArticleInstanceCreator.CreateArticle(name, site, publicationDate) : null;

            if (article != null)
            {
                repository.Create(article);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool CreateBook(string name, string author, int countOfPages)
        {
            //check name, may be we have this skill
            bool uniqueName = !repository.GetAll().Any(x => x.Name.ToLower().Equals(name.ToLower()));
            //if name is unique => create new skill, otherwise skill == null
            Book book = uniqueName ? BookInstanceCreator.CreateBook(name, author, countOfPages) : null;

            if (book != null)
            {
                repository.Create(book);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool UpdateVideo(int id, string name, string link, int quality, int duration)
        {
            Video video = repository.Get(id) as Video;

            if (video == null)
            {
                return false;
            }
            else
            {
                video.Name = name;
                video.Link = link;
                video.Quality = quality;
                video.Duration = duration;
                repository.Update(video);
            }

            return true;
        }

        public bool UpdateArticle(int id, string name, string site, DateTime publicationDate)
        {
            Article article = repository.Get(id) as Article;

            if (article == null)
            {
                return false;
            }
            else
            {
                article.Name = name;
                article.Site = site;
                article.PublicationDate = publicationDate;
                repository.Update(article);
            }

            return true;
        }

        public bool UpdateBook(int id, string name, string author, int countOfPages)
        {
            Book book = repository.Get(id) as Book;

            if (book == null)
            {
                return false;
            }
            else
            {
                book.Name = name;
                book.Author = author;
                book.CountOfPages = countOfPages;
                repository.Update(book);
            }

            return true;
        }

        public IEnumerable<string> GetAllMaterials()
        {
            return repository.GetAll().Select(n => n.ToString());
        }

        public bool Delete(int id)
        {
            Material material = repository.Get(id);

            if (material == null)
            {
                return false;
            }
            else
            {
                repository.Delete(id);
            }

            return true;
        }
    }
}
