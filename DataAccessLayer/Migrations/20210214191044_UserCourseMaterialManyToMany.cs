using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationPortal.DAL.Migrations
{
    public partial class UserCourseMaterialManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserCourses_Id",
                table: "UserCourses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserCourseMaterials",
                columns: table => new
                {
                    UserCourseId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseMaterials", x => new { x.UserCourseId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_UserCourseMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourseMaterials_UserCourses_UserCourseId",
                        column: x => x.UserCourseId,
                        principalTable: "UserCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseMaterials_MaterialId",
                table: "UserCourseMaterials",
                column: "MaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourseMaterials");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserCourses_Id",
                table: "UserCourses");
        }
    }
}
