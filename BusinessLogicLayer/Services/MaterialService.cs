using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EducationPortalConsoleApp.Services
{
    public class MaterialService : IMaterialService
    {
        IRepository<Material> repository;

        public MaterialService(IRepository<Material> repository)
        {
            this.repository = repository;
        }

        public bool CreateVideo(Video video)
        {
            var videos = repository.GetAll().Where(x => x is Video).ToList();

            int c = videos.Count;
            //check name and link, may be we have this skill
            bool uniqueVideo = video != null &&
                !repository.GetAll().Where(x => x is Video).Cast<Video>().Any(x =>
                x.Name.ToLower().Equals(video.Name.ToLower()) &&
                x.Link == video.Link);

            //if name is unique => create new skill, otherwise skill == null
            if (uniqueVideo)
            {
                repository.Create(video);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool CreateArticle(Article article)
        {
            bool uniqueArticle = article != null &&
                !repository.GetAll().Where(x => x is Article).Cast<Article>().Any(x =>
                x.Name.ToLower().Equals(article.Name.ToLower()) &&
                x.PublicationDate == article.PublicationDate &&
                x.Site == article.Site);

            if (uniqueArticle)
            {
                repository.Create(article);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool CreateBook(Book book)
        {
            bool uniqueBook = book != null &&
                !repository.GetAll().Where(x => x is Book).Cast<Book>().Any(x =>
                x.Name.ToLower().Equals(book.Name.ToLower()) &&
                x.Author == book.Author &&
                x.CountOfPages == book.CountOfPages);

            if (uniqueBook)
            {
                repository.Create(book);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool UpdateVideo(Video videoToUpdate)
        {
            Video video = repository.Get(videoToUpdate.Id) as Video;

            if (video == null)
            {
                return false;
            }
            else
            {
                video.Name = videoToUpdate.Name;
                video.Link = videoToUpdate.Link;
                video.Quality = videoToUpdate.Quality;
                video.Duration = videoToUpdate.Duration;
                repository.Update(video);
            }

            return true;
        }

        public bool UpdateArticle(Article articleToUpdate)
        {
            Article article = repository.Get(articleToUpdate.Id) as Article;

            if (article == null)
            {
                return false;
            }
            else
            {
                article.Name = articleToUpdate.Name;
                article.Site = articleToUpdate.Site;
                article.PublicationDate = articleToUpdate.PublicationDate;
                repository.Update(article);
            }

            return true;
        }

        public bool UpdateBook(Book bookToUpdate)
        {
            Book book = repository.Get(bookToUpdate.Id) as Book;

            if (book == null)
            {
                return false;
            }
            else
            {
                book.Name = bookToUpdate.Name;
                book.Author = bookToUpdate.Author;
                book.CountOfPages = bookToUpdate.CountOfPages;
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
