using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.ServicesSql;
using EducationPortal.Domain.Comparers;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EducationPortal.BLL.Tests.ServicesSql
{
    [TestClass]
    public class CourseSqlServiceTests
    {
        private Mock<IRepository<Course>> courseRepository;
        private Mock<ICourseMaterialService> courseMaterailService;
        private Mock<ICourseSkillService> courseSkillService;
        private Mock<IMaterialService> materailService;
        private Mock<ISkillService> skillService;
        private Mock<ICourseComparerService> courseComparerService;

        [TestInitialize]
        public void SetUp()
        {
            this.courseRepository = new Mock<IRepository<Course>>();
            this.courseMaterailService = new Mock<ICourseMaterialService>();
            this.courseSkillService = new Mock<ICourseSkillService>();
            this.materailService = new Mock<IMaterialService>();
            this.skillService = new Mock<ISkillService>();
            this.courseComparerService = new Mock<ICourseComparerService>();
        }

        #region AddMaterialToCourseTests

        [TestMethod]
        public void AddMaterialToCourse_CourseNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);
            materailService.Setup(db => db.ExistMaterial(It.IsAny<int>())).Returns(true);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Material material = new Material();

            Assert.IsFalse(courseSqlService.AddMaterialToCourse(0, material));
        }

        [TestMethod]
        public void AddMaterialToCourse_SkillNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            materailService.Setup(db => db.ExistMaterial(It.IsAny<int>())).Returns(false);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Material material = new Material();

            Assert.IsFalse(courseSqlService.AddMaterialToCourse(0, material));
        }

        [TestMethod]
        public void AddMaterialToCourse_CourseAndSkillNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);
            materailService.Setup(db => db.ExistMaterial(It.IsAny<int>())).Returns(false);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Material material = new Material();

            Assert.IsFalse(courseSqlService.AddMaterialToCourse(0, material));
        }

        [TestMethod]
        public void AddMaterialToCourse_CourseAndSkillExist_True()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            materailService.Setup(db => db.ExistMaterial(It.IsAny<int>())).Returns(true);
            courseMaterailService.Setup(db => db.AddMaterialToCourse(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Material material = new Material();

            Assert.IsTrue(courseSqlService.AddMaterialToCourse(0, material));
        }

        #endregion

        #region AddSkillToCourseTests

        [TestMethod]
        public void AddSkillToCourse_CourseNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);
            skillService.Setup(db => db.ExistSkill(It.IsAny<int>())).Returns(true);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Skill skill = new Skill();

            Assert.IsFalse(courseSqlService.AddSkillToCourse(0, skill));
        }

        [TestMethod]
        public void AddSkillToCourse_SkillNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            skillService.Setup(db => db.ExistSkill(It.IsAny<int>())).Returns(false);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Skill skill = new Skill();

            Assert.IsFalse(courseSqlService.AddSkillToCourse(0, skill));
        }

        [TestMethod]
        public void AddSkillToCourse_CourseAndSkillNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);
            skillService.Setup(db => db.ExistSkill(It.IsAny<int>())).Returns(false);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Skill skill = new Skill();

            Assert.IsFalse(courseSqlService.AddSkillToCourse(0, skill));
        }

        [TestMethod]
        public void AddSkillToCourse_CourseAndSkillExist_True()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            skillService.Setup(db => db.ExistSkill(It.IsAny<int>())).Returns(true);
            courseSkillService.Setup(db => db.AddSkillToCourse(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Skill skill = new Skill();

            Assert.IsTrue(courseSqlService.AddSkillToCourse(0, skill));
        }

        #endregion

        #region CreateCourseTests

        [TestMethod]
        public void CreateCourse_CourseNull_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);
            Course course = null;

            Assert.IsFalse(courseSqlService.CreateCourse(course));
        }

        [TestMethod]
        public void CreateCourse_CourseExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);
            Course course = new Course();

            Assert.IsFalse(courseSqlService.CreateCourse(course));
        }

        [TestMethod]
        public void CreateCourse_CourseNotNullAndCourseNotExist_True()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);
            courseRepository.Setup(db => db.Save());

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);
            
            Course course = new Course();
            courseSqlService.CreateCourse(course);

            courseRepository.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(courseSqlService.CreateCourse(course));
        }

        #endregion

        #region Delete

        [TestMethod]
        public void Delete_CourseNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Assert.IsFalse(courseSqlService.Delete(0));
        }

        [TestMethod]
        public void Delete_CourseExist_True()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            courseRepository.Setup(db => db.Delete(It.IsAny<int>()));
            courseRepository.Setup(db => db.Save());

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            courseSqlService.Delete(0);

            courseRepository.Verify(x => x.Delete(0), Times.Once);
            courseRepository.Verify(x => x.Save(), Times.Once);
            Assert.IsTrue(courseSqlService.Delete(0));
        }

        #endregion

        #region GetMaterialsFromCourse

        [TestMethod]
        public void GetMaterialsFromCourse_CourseExist_CallMaterialAndCourseMaterialServices()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            courseMaterailService.Setup(db => db.GetAllMaterialsFromCourse(It.IsAny<int>())).Returns(new List<Material>());
            materailService.Setup(db => db.GetAllNotPassedMaterialFromUser()).Returns(new List<Material>());

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            courseSqlService.GetMaterialsFromCourse(0);

            courseMaterailService.Verify(x => x.GetAllMaterialsFromCourse(0), Times.Once);
            materailService.Verify(x => x.GetAllNotPassedMaterialFromUser(), Times.Once);
        }

        [TestMethod]
        public void GetMaterialsFromCourse_CourseNotExist_ReturnNull()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);
            courseMaterailService.Setup(db => db.GetAllMaterialsFromCourse(It.IsAny<int>())).Returns(new List<Material>());
            materailService.Setup(db => db.GetAllNotPassedMaterialFromUser()).Returns(new List<Material>());

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Assert.IsNull(courseSqlService.GetMaterialsFromCourse(0));
        }

        #endregion

        #region GetSkillsFromCourse

        [TestMethod]
        public void GetSkillsFromCourse_CourseExist_CallCourseSkillService()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            courseSkillService.Setup(db => db.GetAllSkillsFromCourse(It.IsAny<int>())).Returns(new List<Skill>());

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            courseSqlService.GetSkillsFromCourse(0);

            courseSkillService.Verify(x => x.GetAllSkillsFromCourse(0), Times.Once);
        }

        [TestMethod]
        public void GetSkillsFromCourse_CourseNotExist_Null()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);
            courseSkillService.Setup(db => db.GetAllSkillsFromCourse(It.IsAny<int>())).Returns(new List<Skill>());

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Assert.IsNull(courseSqlService.GetSkillsFromCourse(0));
        }

        #endregion

        #region UpdateCourse

        [TestMethod]
        public void UpdateCourse_CourseNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);
            courseRepository.Setup(db => db.Update(It.IsAny<Course>()));
            courseRepository.Setup(db => db.Save());

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Course course = new Course();

            Assert.IsFalse(courseSqlService.UpdateCourse(course));
        }

        [TestMethod]
        public void UpdateCourse_CourseExist_UpdateCourse()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);
            courseRepository.Setup(db => db.Update(It.IsAny<Course>()));
            courseRepository.Setup(db => db.Save());

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Course course = new Course()
            {
                Name = "1",
                Id = 0,
                Description = "desc",
                Materials = new List<Material>(),
                Skills = new List<Skill>()
            };

            courseSqlService.UpdateCourse(course);
            courseRepository.Verify(x => x.Update(course));
            courseRepository.Verify(x => x.Save());
            Assert.IsTrue(courseSqlService.UpdateCourse(course));
        }

        #endregion

        #region ExistCourse

        [TestMethod]
        public void ExistCourse_CourseExist_True()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(true);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Assert.IsTrue(courseSqlService.ExistCourse(0));
        }

        [TestMethod]
        public void ExistCourse_CourseNotExist_False()
        {
            courseRepository.Setup(db => db.Exist(It.IsAny<Expression<Func<Course, bool>>>())).Returns(false);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            Assert.IsFalse(courseSqlService.ExistCourse(0));
        }

        #endregion

        #region AvailableCourses

        [TestMethod]
        public void AvailableCourses_ListNull_Null()
        {
            courseRepository.Setup(db => db.Except(It.IsAny<List<Course>>(), It.IsAny<CourseComparer>())).Returns(new List<Course>());

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            List<Course> courses = null;

            Assert.IsNull(courseSqlService.AvailableCourses(courses));
        }

        [TestMethod]
        public void AvailableCourses_ListNotNull_ReturnList()
        {
            courseRepository.Setup(db => db.Except(It.IsAny<List<Course>>(), It.IsAny<CourseComparer>())).Returns(new List<Course>());
            courseComparerService.Setup(db => db.CourseComparer);

            CourseSqlService courseSqlService = new CourseSqlService(courseRepository.Object,
                courseMaterailService.Object, courseSkillService.Object, materailService.Object, skillService.Object, courseComparerService.Object);

            List<Course> courses = new List<Course>();

            courseSqlService.AvailableCourses(courses);
            courseComparerService.Verify(x => x.CourseComparer, Times.Once);
            courseRepository.Verify(x => x.Except(courses, courseComparerService.Object.CourseComparer), Times.Once);
        }

        #endregion
    }
}
