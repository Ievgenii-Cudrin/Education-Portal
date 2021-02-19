namespace BusinessLogicLayer.Interfaces
{
    using System.Collections.Generic;
    using Entities;

    public interface IMaterialService
    {
        Material CreateMaterial(Material material);

        bool UpdateVideo(Video video);

        bool UpdateArticle(Article article);

        bool UpdateBook(Book book);

        IEnumerable<Material> GetAllMaterials();

        Material GetMaterial(int id);

        bool Delete(int id);
    }
}
