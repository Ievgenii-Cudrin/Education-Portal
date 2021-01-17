using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EducationPortalConsoleApp.InstanceCreator;

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
            //check name, may be we have this skill
            //bool uniqueName = video != null ? !repository.GetAll().Any(x => x.Name.ToLower().Equals(video.Name.ToLower())) : false;

            //if name is unique => create new skill, otherwise skill == null
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

        public bool CreateArticle(Article article)
        {
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

        public bool CreateBook(Book book)
        {
            //check name, may be we have this skill
            //bool uniqueName = !repository.GetAll().Any(x => x.Name.ToLower().Equals(bookToCreate.Name.ToLower()));
            //if name is unique => create new skill, otherwise skill == null
            //Book book = uniqueName ? BookInstanceCreator.CreateBook(name, author, countOfPages) : null;

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
