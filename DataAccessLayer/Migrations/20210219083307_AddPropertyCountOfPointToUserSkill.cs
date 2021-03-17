using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationPortal.DAL.Migrations
{
    public partial class AddPropertyCountOfPointToUserSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfPoint",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "CountOfPoint",
                table: "UserSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfPoint",
                table: "UserSkills");

            migrationBuilder.AddColumn<int>(
                name: "CountOfPoint",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
