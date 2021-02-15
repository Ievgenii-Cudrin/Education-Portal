namespace EducationPortal.Domain.Entities
{
    using DataAccessLayer.Entities;
    using global::Entities;

    public class UserMaterial
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }
    }
}
