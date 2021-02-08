namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using Entities;

    public interface IMaterialService : IDeleteEntity
    {
        public bool CreateVideo(Video video);

        public bool CreateArticle(Article article);

        public bool CreateBook(Book book);

        public bool UpdateVideo(Video video);

        public bool UpdateArticle(Article article);

        public bool UpdateBook(Book book);

        public IEnumerable<Material> GetAllMaterials();

        public Material GetMaterial(int id);
    }
}
