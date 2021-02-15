namespace EducationPortal.Domain.Entities
{
    using DataAccessLayer.Entities;

    public class CourseSkill
    {
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public int SkillId { get; set; }

        public Skill Skill { get; set; }
    }
}
