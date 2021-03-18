using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationPortal.DAL.Migrations
{
    public partial class TransferPropertyIsPassedToAnotherMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPassed",
                table: "Materials");

            migrationBuilder.AddColumn<bool>(
                name: "IsPassed",
                table: "UserCourseMaterials",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPassed",
                table: "UserCourseMaterials");

            migrationBuilder.AddColumn<bool>(
                name: "IsPassed",
                table: "Materials",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
