namespace EducationPortalConsoleApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Interfaces;
    using DataAccessLayer.Repositories;
    using Entities;

    public class MaterialService// : IMaterialService
    {
        private readonly IRepository<Material> repository;
        private readonly List<Material> materialsFromDB;

        public MaterialService(IEnumerable<IRepository<Material>> repositories)
        {
            this.repository = repositories.FirstOrDefault(t => t.GetType() == typeof(RepositoryXml<Material>));
            this.materialsFromDB = repository.GetAll().ToList();
        }

        public bool CreateVideo(Video video)
        {
            var videos = this.repository.GetAll().Where(x => x is Video).ToList();

            // check name and link, may be we have this skill
            bool uniqueVideo = video != null &&
                !this.materialsFromDB.Where(x => x is Video).Cast<Video>().Any(x =>
                x.Name.ToLower().Equals(video.Name.ToLower()) &&
                x.Link == video.Link);

            return this.SaveMaterialToDB(uniqueVideo, video);
        }

        public bool CreateArticle(Article article)
        {
            // check, it is unique article in db
            bool uniqueArticle = article != null &&
                !this.materialsFromDB.Where(x => x is Article).Cast<Article>().Any(x =>
                x.Name.ToLower().Equals(article.Name.ToLower()) &&
                x.PublicationDate == article.PublicationDate &&
                x.Site == article.Site);

            return this.SaveMaterialToDB(uniqueArticle, article);
        }

        public bool CreateBook(Book book)
        {
            // check, it is unique book in db
            bool uniqueBook = book != null &&
                !this.materialsFromDB.Where(x => x is Book).Cast<Book>().Any(x =>
                x.Name.ToLower().Equals(book.Name.ToLower()) &&
                x.Author == book.Author &&
                x.CountOfPages == book.CountOfPages);

            return this.SaveMaterialToDB(uniqueBook, book);
        }

        public bool UpdateVideo(Video videoToUpdate)
        {
            if (!(this.repository.Get(videoToUpdate.Id) is Video video))
            {
                return false;
            }
            else
            {
                video.Name = videoToUpdate.Name;
                video.Link = videoToUpdate.Link;
                video.Quality = videoToUpdate.Quality;
                video.Duration = videoToUpdate.Duration;
                this.repository.Update(video);
            }

            return true;
        }

        public bool UpdateArticle(Article articleToUpdate)
        {
            if (!(this.repository.Get(articleToUpdate.Id) is Article article))
            {
                return false;
            }
            else
            {
                article.Name = articleToUpdate.Name;
                article.Site = articleToUpdate.Site;
                article.PublicationDate = articleToUpdate.PublicationDate;
                this.repository.Update(article);
            }

            return true;
        }

        public bool UpdateBook(Book bookToUpdate)
        {
            if (!(this.repository.Get(bookToUpdate.Id) is Book book))
            {
                return false;
            }
            else
            {
                book.Name = bookToUpdate.Name;
                book.Author = bookToUpdate.Author;
                book.CountOfPages = bookToUpdate.CountOfPages;
                this.repository.Update(book);
            }

            return true;
        }

        public IEnumerable<Material> GetAllExceptedMaterials()
        {
            return this.repository.GetAll();
        }

        public bool Delete(int id)
        {
            Material material = this.repository.Get(id);

            if (material == null)
            {
                return false;
            }
            else
            {
                this.repository.Delete(id);
            }

            return true;
        }

        public Material GetMaterial(int id)
        {
            Material material;

            try
            {
                material = this.repository.Get(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return material;
        }

        private bool SaveMaterialToDB(bool uniqueMaterial, Material material)
        {
            if (uniqueMaterial)
            {
                this.repository.Add(material);
                this.materialsFromDB.Add(material);
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
