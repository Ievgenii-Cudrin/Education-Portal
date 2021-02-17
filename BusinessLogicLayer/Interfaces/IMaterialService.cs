namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using Entities;

    public interface IMaterialService
    {
        bool CreateVideo(Video video);

        bool CreateArticle(Article article);

        bool CreateBook(Book book);

        bool UpdateVideo(Video video);

        bool UpdateArticle(Article article);

        bool UpdateBook(Book book);

        IEnumerable<Material> GetAllMaterials();

        Material GetMaterial(int id);

        bool Delete(int id);
    }
}
