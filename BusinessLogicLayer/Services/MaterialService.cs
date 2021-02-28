namespace EducationPortalConsoleApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogicLayer.Interfaces;
    using DataAccessLayer.Entities;
    using DataAccessLayer.Interfaces;
    using EducationPortal.BLL.Interfaces;
    using EducationPortal.DAL.XML.Repositories;
    using Entities;

    public class MaterialService : IMaterialService
    {
        private readonly IRepository<Material> materialRepository;
        private readonly List<Material> materialsFromDB;
        private readonly IAuthorizedUser authorizedUser;
        private readonly IMaterialComparerService materialComparerService;
        private readonly ICourseService courseService;

        public MaterialService(
            IRepository<Material> repositories,
            IAuthorizedUser authorizedUser,
            IMaterialComparerService materialComparerService,
            ICourseService courseService)
        {
            this.materialRepository = repositories;
            this.materialsFromDB = materialRepository.GetAll().ToList();
            this.authorizedUser = authorizedUser;
            this.materialComparerService = materialComparerService;
            this.courseService = courseService;
        }

        public Material CreateMaterial(Material material)
        {
            if (material != null && !this.materialRepository.Exist(x => x.Name == material.Name))
            {
                this.materialRepository.Add(material);
                this.materialsFromDB.Add(material);
                return material;
            }

            return null;
        }

        //public bool UpdateVideo(Video videoToUpdate)
        //{
        //    if (!(this.materialRepository.Get(videoToUpdate.Id) is Video video))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        video.Name = videoToUpdate.Name;
        //        video.Link = videoToUpdate.Link;
        //        video.Quality = videoToUpdate.Quality;
        //        video.Duration = videoToUpdate.Duration;
        //        this.materialRepository.Update(video);
        //    }

        //    return true;
        //}

        //public bool UpdateArticle(Article articleToUpdate)
        //{
        //    if (!(this.materialRepository.Get(articleToUpdate.Id) is Article article))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        article.Name = articleToUpdate.Name;
        //        article.Site = articleToUpdate.Site;
        //        article.PublicationDate = articleToUpdate.PublicationDate;
        //        this.materialRepository.Update(article);
        //    }

        //    return true;
        //}

        //public bool UpdateBook(Book bookToUpdate)
        //{
        //    if (!(this.materialRepository.Get(bookToUpdate.Id) is Book book))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        book.Name = bookToUpdate.Name;
        //        book.Author = bookToUpdate.Author;
        //        book.CountOfPages = bookToUpdate.CountOfPages;
        //        this.materialRepository.Update(book);
        //    }

        //    return true;
        //}

        public IEnumerable<Material> GetAllExceptedMaterials()
        {
            return this.materialRepository.GetAll();
        }

        public bool Delete(int id)
        {
            Material material = this.materialRepository.Get(id);

            if (material == null)
            {
                return false;
            }
            else
            {
                this.materialRepository.Delete(id);
            }

            return true;
        }

        public Material GetMaterial(int id)
        {
            Material material;

            try
            {
                material = this.materialRepository.Get(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return material;
        }

        public IEnumerable<Material> GetAllExceptedMaterials(int courseId)
        {
            return this.materialRepository.Except(this.courseService.GetMaterialsFromCourse(courseId), this.materialComparerService.MaterialComparer);
        }

        public bool ExistMaterial(int materialId)
        {
            return this.materialRepository.Exist(x => x.Id == materialId);
        }

        public IEnumerable<Material> GetAllNotPassedMaterialFromUser()
        {
            return this.materialRepository.Except(this.authorizedUser.User.PassedMaterials, this.materialComparerService.MaterialComparer);
        }
    }
}
