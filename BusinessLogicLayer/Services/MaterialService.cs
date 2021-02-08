﻿using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities;

namespace EducationPortalConsoleApp.Services
{
    public class MaterialService : IMaterialService
    {
        IRepository<Material> repository;
        List<Material> materialsFromDB;
        public MaterialService(IRepository<Material> repository)
        {
            this.repository = repository;
            materialsFromDB = repository.GetAll().ToList();
        }

        public bool CreateVideo(Video video)
        {
            var videos = repository.GetAll().Where(x => x is Video).ToList();

            int c = videos.Count;
            //check name and link, may be we have this skill
            bool uniqueVideo = video != null &&
                !materialsFromDB.Where(x => x is Video).Cast<Video>().Any(x =>
                x.Name.ToLower().Equals(video.Name.ToLower()) &&
                x.Link == video.Link);
            
            return SaveMaterialToDB(uniqueVideo, video);
        }

        public bool CreateArticle(Article article)
        {
            //check, it is unique article in db
            bool uniqueArticle = article != null &&
                !materialsFromDB.Where(x => x is Article).Cast<Article>().Any(x =>
                x.Name.ToLower().Equals(article.Name.ToLower()) &&
                x.PublicationDate == article.PublicationDate &&
                x.Site == article.Site);

            return SaveMaterialToDB(uniqueArticle, article);
        }

        public bool CreateBook(Book book)
        {
            //check, it is unique book in db
            bool uniqueBook = book != null &&
                !materialsFromDB.Where(x => x is Book).Cast<Book>().Any(x =>
                x.Name.ToLower().Equals(book.Name.ToLower()) &&
                x.Author == book.Author &&
                x.CountOfPages == book.CountOfPages);

            return SaveMaterialToDB(uniqueBook, book);
        }

        private bool SaveMaterialToDB(bool uniqueMaterial, Material material)
        {
            if (uniqueMaterial)
            {
                repository.Create(material);
                materialsFromDB.Add(material);
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

        public IEnumerable<Material> GetAllMaterials()
        {
            return repository.GetAll();
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

        public Material GetMaterial(int id)
        {
            Material material;
            try
            {
                material = repository.Get(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return material;
        }
    }
}
