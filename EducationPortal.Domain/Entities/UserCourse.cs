namespace EducationPortal.Domain.Entities
{
    using DataAccessLayer.Entities;

    public class UserCourse
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
